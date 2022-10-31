using PathGame.Interface;
using UnityEngine;

namespace PathGame.Level
{
    public class CellMarker : MonoBehaviour
    {
        [SerializeField] private GameObject _prefub;
        
        [SerializeField] private Color _editorFull;
        [SerializeField] private Color _rampeEditorFull;
        [SerializeField] private Color _editorEmpty;
        [SerializeField] private Color _rampeEditorEmpty;
        private ICell _cellCache;
        private bool isInit = false;
        public GameObject GetPrefub() => _prefub;
        public ICell GetCell()
        {
            if (_cellCache == null)
            {
                _cellCache = _prefub.GetComponent<ICell>();
                if (_cellCache == null)
                {
                    Debug.Log("Prefub not implementation component ICell");
                    return null;
                }
            }

            return _cellCache;
        }

        public ICell Init(int index)
        {
            _prefub = Instantiate(_prefub, transform.position, transform.rotation, transform);
            isInit = true;
            gameObject.name = "Marker : " + index;
            
            var cell = _prefub.GetComponent<ICell>();
            cell.Index = index;
            return cell;
        }
        private void OnDrawGizmosSelected()
        {
            
            Gizmos.color = GetCell().CellEmpty() ? _editorEmpty : _editorFull;
            Gizmos.DrawCube(transform.position, GetCell().GetSizeCell());
            Gizmos.color = GetCell().CellEmpty() ? _rampeEditorEmpty : _rampeEditorFull;
            Gizmos.DrawWireCube(transform.position, GetCell().GetSizeCell());
        }
    }
}