using System;
using PathGame.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PathGame.Level
{
    public class GridField2D : MonoBehaviour, IGridField
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private CellMarker _cellMarker;
        [SerializeField] private Transform _platform;
        private ICell[,] _cells;
        
        private Vector3 _cellSize;
        private IGridField _gridFieldImplementation;
        
        
        [Button("Rebuild Grid")]
        public void RebuildMarkers()
        {
            foreach (var marker in GetComponentsInChildren<CellMarker>())
                DestroyImmediate(marker.gameObject);
            
            _cells = new ICell[_width, _height];
            _cellSize = _cellMarker.GetCell().GetSizeCell();
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    _cells[i,j] = Instantiate(_cellMarker.gameObject, transform.position + new Vector3(i * _cellSize.x, 0f, j * _cellSize.z),
                        Quaternion.identity, transform).GetComponent<CellMarker>().Init(i + j * _width);
                }
            }
        }

        private void OnValidate()
        {
            _cellSize = _cellMarker.GetCell().GetSizeCell();
            if (_platform != null)
            {
                _platform.localScale = new Vector3(_cellSize.x * _width,_cellSize.y * _height, _cellSize.z * _height); 
                _platform.transform.position = transform.position + new Vector3(_platform.localScale.x,0f, _platform.localScale.z)/2f - _cellSize / 2f;
            }
        }

        private void Awake()
        {
            _cells = new ICell[_width, _height];
            _cellSize = _cellMarker.GetCell().GetSizeCell();
            
            foreach (var cell in GetComponentsInChildren<ICell>())
                SetCell(cell.Index, cell);
            
        }

        public ICell GetCell(Vector3 position)
        {
            foreach (var cell in _cells)
                if(cell.Contains(position))
                    return cell;
            
            return null;
        }

        public Vector3 GetCellSize() => _cellSize;
        
        public int Count => _cells.Length;

        public GameObject GetCell(int index) => _cells[index % _width, index / _width].gameObject;
        public event Action<int> cellUpdate;
        public void CellUpdate(int index) => cellUpdate?.Invoke(index);
        private void SetCell(int index, ICell value) => _cells[index % _width, index / _width] = value;
    }
}
