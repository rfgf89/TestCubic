using System;
using PathGame.Interface;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace PathGame.Level
{
    public class CellRect : MonoBehaviour, ICell
    {
        public int index;
        [SerializeField] private Vector3 size;
        [SerializeField] private CellValueBehaviour _cellObject = null;
        
        [ReadOnly] public Bounds cellBounds;
        [SerializeField, ReadOnly] private bool _isFree = true;
        private CellValueBehaviour _prevCellObject;
        private IGridField _gridField;
        
        [Inject]
        private void Construct(IGridField gridField)
        {
            _gridField = gridField;
        }
        public int Index { get => index; set => index = value; }
        private void Start()
        {
            cellBounds = new Bounds(transform.position, size);
        }
        public void SetItem(CellValueBehaviour cellObject)
        {
            
            if (cellObject!=null)
            {
                if (!cellObject.gameObject.activeInHierarchy)
                {
#if UNITY_EDITOR
                    var org = cellObject;
                    cellObject = ((GameObject)PrefabUtility.InstantiatePrefab(cellObject.gameObject, transform)).GetComponent<CellValueBehaviour>();
                    cellObject.prefabOriginal = org.transform;
#endif
                }
                else
                {
                    if(_cellObject!=null)
                        _cellObject.transform.position = transform.position + size/2 - cellObject.prefabOriginal.transform.localPosition;
                }
            }

            _cellObject = cellObject;
            if(_cellObject == null)
                _isFree = true;
            else
            {
                cellObject.transform.position = transform.position - size/2 + cellObject.prefabOriginal.transform.localPosition;
                
                _isFree = false;
            }
            
            _gridField?.CellUpdate(index);
        }
        public GameObject GetItem()
        {
            Transform cell = null;
           
            try
            { 
                cell = _cellObject.GetComponent<Transform>();
            }
            catch (Exception e)
            {
                return null;
            }
            
            if (cell == null)
                return null;
            
            return cell.gameObject;
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_cellObject != _prevCellObject)
            {
                if (_prevCellObject != null)
                    DestroyImmediate(_prevCellObject.gameObject);
                
                SetItem(_cellObject);
            }
            
            _isFree = _cellObject == null;

            _prevCellObject = _cellObject;
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position + new Vector3(-size.x/2f, 0f, size.y/4f),index.ToString());
        }
#endif
        public Vector3 GetSizeCell() => size;
        
        public bool CellEmpty() => _isFree;

        public bool Contains(Vector3 position) => cellBounds.Contains(position);
        
    }
}