using System;
using System.Collections.Generic;
using PathGame.Data;
using PathGame.Enum;
using PathGame.Interface;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace PathGame.Level
{
    public class EntryPoint : CellValueBehaviour, IEntryPoint, IPathState
    {
        [SerializeField] private bool _isPathFinding;
       
        [SerializeField] private Transform entryPoint;
        [SerializeField] private string _tag;
        [SerializeField] private VisualPathSegment[] _visualPathSegments;
        public bool isPathFinding { get; }
        public event Action<List<ListEntryPoint>, bool> pathUpdate;
        public string Tag => _tag;
        private List<ListEntryPoint> _currentPath = new List<ListEntryPoint>();
        private IEntryPoint _startPathPoint;
        public List<ListEntryPoint> PathFind()
        {
            if (_isPathFinding)
            {
                ChangeState(PathState.Close);
                foreach (var entry in _currentPath)
                    entry.PathChangeState(PathState.Close);
                _startPathPoint?.SetPath(new List<ListEntryPoint>());
                _startPathPoint = null;
                _currentPath.Clear();

                Vector3 position = transform.position;
                ListEntryPoint current = NearCellPath(ref position, entryPoint.position);

                if (current != null)
                {
                    _currentPath.Add(current);
                    while (true)
                    {
                        current = NearCellPath(ref position, current.lines[^1].position);
                        if (current == null)
                            break;

                        _currentPath.Add(current);
                    }
                }

                if (_currentPath != null && _startPathPoint != null)
                {
                    foreach (var listEntry in _currentPath)
                        listEntry.PathChangeState(PathState.Open);
                    if (_currentPath.Count > 0)
                        ChangeState(PathState.Open);
                        _startPathPoint.SetPath(_currentPath);

                    pathUpdate?.Invoke(_currentPath, true);
                }
            }

            return _currentPath;
        }

        public void SetPath(List<ListEntryPoint> path)
        {
            _currentPath = path;
            if(_currentPath.Count == 0)
                ChangeState(PathState.Close);
            else
            {
                ChangeState(PathState.Open);
            }
            pathUpdate?.Invoke(_currentPath, false);
        }
        
        private ListEntryPoint NearCellPath(ref Vector3 position, Vector3 endPrevious)
        {
            var cells = new ICell[]
            {
                _gridField.GetCell(position + Vector3.forward * _gridField.GetCellSize().z),
                _gridField.GetCell(position + Vector3.back * _gridField.GetCellSize().z),
                _gridField.GetCell(position + Vector3.right * _gridField.GetCellSize().x),
                _gridField.GetCell(position + Vector3.left * _gridField.GetCellSize().x)
            };

            IPath path;
            ListEntryPoint entryPointLine;
            foreach (var cell in cells)
            {
                if (cell == null || cell.CellEmpty()) continue;
                path = cell.GetItem().GetComponent<IPath>();

                if (path != null)
                {
                    entryPointLine = path.GetNear(position);
                    
                    if (entryPointLine != null)
                    {
                        if (endPrevious != null )
                        {
                            if (entryPointLine.entryPoint.position == endPrevious)
                            {
                                position = cell.gameObject.transform.position;
                                return entryPointLine;
                            }
                        }
                    }
                }
                else
                {
                    _startPathPoint = cell
                        .GetItem()
                        .GetComponent<IEntryPoint>();
                    
                    if (_startPathPoint != null && (_startPathPoint.Tag != _tag || _startPathPoint.isPathFinding))
                        _startPathPoint = null;
                }
            }

            return null;
        }
        
        public void ChangeState(PathState state)
        {
            foreach (var val in _visualPathSegments)
                val.ChangeState(state);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.Label(transform.position + Vector3.up, Tag);
            var pos = transform.position + Vector3.up;
            var pos2 = new Vector3(entryPoint.position.x,transform.position.y, entryPoint.position.z) + Vector3.up;
            
            if(_isPathFinding)
                HandleLineThickness(pos2, pos2 - pos, new Color(0.5f,0.5f,1f,1f),0.3f);
            else
                HandleLineThickness(pos2, pos - pos2, new Color(0.5f,0.5f,1f,1f),0.3f);
        }
        
        public static void HandleLineThickness(Vector3 pos, Vector3 direction, Color color, float thickness, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            thickness /= math.distance(Camera.current.transform.position, pos)/50f;

            Handles.color = color;
            Handles.DrawLine(pos, pos + direction, thickness);
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
            Handles.DrawLine(pos + direction, pos - right * arrowHeadLength, thickness);
            Handles.DrawLine(pos + direction, pos - left * arrowHeadLength, thickness);
            
            Handles.color = Color.white;
        }
#endif
    }

}
