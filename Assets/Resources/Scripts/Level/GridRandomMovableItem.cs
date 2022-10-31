using System.Collections.Generic;
using PathGame.Interface;
using PathGame.Level;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class GridRandomMovableItem : MonoBehaviour
{
    [SerializeField] private bool isStart;

    private IGridField _gridField;

    [Inject]
    private void Construct(IGridField gridField)
    {
        _gridField = gridField;
    }
    private void Start()
    {
        _gridField = GetComponent<GridField2D>();
        if (isStart)
        {
            isStart = false;
            ICell currenCell;
            List<GameObject> item = new List<GameObject>(8);
            List<int> cellFree = new List<int>(8);
            for (int i = 0; i < _gridField.Count; i++)
            {
                currenCell = _gridField.GetCell(i).GetComponent<ICell>();
                if (currenCell.GetItem()?.GetComponent<ICellItemMove>() != null)
                {
                    item.Add(currenCell.GetItem());
                    currenCell.SetItem(null);
                }
            }

            for (int i = 0; i < _gridField.Count; i++)
            {
                currenCell = _gridField.GetCell(i).GetComponent<ICell>();
                if (currenCell.GetItem() == null)
                    cellFree.Add(i);
            }

            int curr;
            GameObject currItem;
            int random;
            while (item.Count > 0)
            {
                random = Random.Range(0, cellFree.Count);
                curr = cellFree[random];
                currItem = item[^1];
                cellFree.RemoveAt(random);
                item.RemoveAt(item.Count - 1);
                
                _gridField.GetCell(curr).GetComponent<ICell>().SetItem(currItem.GetComponent<CellValueBehaviour>());
            }
        }
    }
}
