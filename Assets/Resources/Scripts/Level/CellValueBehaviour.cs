using PathGame.Interface;
using UnityEngine;
using Zenject;

namespace PathGame.Level
{
    public class CellValueBehaviour : MonoBehaviour
    {
        protected IGridField _gridField;
        [HideInInspector]public Transform prefabOriginal;
        
        [Inject]
        private void Construct(IGridField gridField)
        {
            _gridField = gridField;
        }
        
    }
}