using PathGame.Input;
using PathGame.Interface;
using PathGame.Level;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace PathGame.Player
{
    public class PlayerFieldSelector : MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private IGridField _gridField;
        private Vector2 _selectorScreenPos = Vector2.zero;
        private ICell _cellSelect;
        private ICell _cellItemTemp;
        private RaycastHit _hit;
        private ICellItemMove _moveComp;
        private readonly Vector3 _offset = new Vector3(0.0f, 0.01f, 0.0f);
        private readonly Vector3 _offsetTempItem = new Vector3(0.0f, 1.0f, 0.0f);
        [Inject]
        private void Construct(IPlayerInput playerInput, IGridField gridField)
        {
            _playerInput = playerInput;
            _gridField = gridField;
        }

        public void Start()
        {
            _playerInput.AddListener(PathGamePlayerInput.InputType.SelectorClick, SelectorClick, null, null);
            _playerInput.AddListener(PathGamePlayerInput.InputType.SelectorPosition, null, SelectorPosition, null);
            PathFindAllEntryPoint();
        }

        private void SelectorPosition(InputAction.CallbackContext obj)
        {
            _selectorScreenPos = obj.ReadValue<Vector2>();
            
            var ray = Camera.main.ScreenPointToRay(_selectorScreenPos);

            if (Physics.Raycast(ray, out _hit) && _moveComp != null && _gridField.GetCell(_hit.point + _offset) != null)
                _moveComp.transform.position = 
                    math.floor((float3)(_hit.point + _offsetTempItem )
                               /_gridField.GetCellSize())*_gridField.GetCellSize() + (float3)_gridField.GetCellSize()/2f ;
        }

        private void SelectorClick(InputAction.CallbackContext obj)
        {
            _cellSelect = _gridField.GetCell(_hit.point + _offset);
            
            if (_cellSelect != null)
            {
                if (_moveComp == null)
                {
                    if (!_cellSelect.CellEmpty())
                    {
                        var moveComp = _cellSelect?.GetItem()?.GetComponent<ICellItemMove>();
                        if (moveComp != null)
                        {
                            _cellSelect.SetItem(null);
                            _moveComp = moveComp;
                            PathFindAllEntryPoint();
                        }
                    }
                }
                else if (_cellSelect.CellEmpty())
                {
                    _cellSelect.SetItem(_moveComp.transform.GetComponent<CellValueBehaviour>());
                    _moveComp = null;
                    PathFindAllEntryPoint();
                }
            }
        }

        private void PathFindAllEntryPoint()
        {
            for (int i = 0; i < _gridField.Count; i++)
            {
                _gridField
                    .GetCell(i)
                    .GetComponent<ICell>()?
                    .GetItem()?
                    .GetComponent<IEntryPoint>()?
                    .PathFind();
            }
            
        }
        private void OnDrawGizmos()
        {
            if (_cellSelect != null)
            {
                
                Gizmos.color = Color.cyan/2f;
                Gizmos.DrawWireCube(_cellSelect.gameObject.transform.position, _cellSelect.GetSizeCell()*1.2f);
                Gizmos.color = Color.white;
            }
        }
    }
}