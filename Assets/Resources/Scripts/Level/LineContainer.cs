using System;
using PathGame.Data;
using PathGame.Enum;
using PathGame.Interface;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace PathGame.Level
{
    public class LineContainer : CellValueBehaviour, IPathState, ICellItemMove, IPath
    {
        [SerializeField] private ListEntryPoint[] _entryLines;
        [SerializeField] private VisualPathSegment[] _visualPathSegments;
        
        private void OnEnable()
        {
            foreach (var entryPoint in _entryLines)
                entryPoint.pathOpen += ChangeState;
        }

        private void OnDisable()
        {
            foreach (var entryPoint in _entryLines)
                entryPoint.pathOpen -= ChangeState;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (var lines in _entryLines)
            {
                if (lines.lines.Length > 0)
                {
                    var line = lines.entryPoint;

                    for (int i = 0; i < lines.lines.Length; i++)
                    {
                        Handles.DrawBezier(line.position, lines.lines[i].position,
                            line.position, lines.lines[i].position, Color.white, null, 1.0f);

                        line = lines.lines[i];
                    }
                }
            }
        }
#endif
        public void ChangeState(PathState state)
        {
            foreach (var listEntry in _entryLines)
                listEntry.center = transform.position;

            foreach (var val in _visualPathSegments)
                val.ChangeState(state);
        }

        public Bounds cellBounds { get; }
        

        public ListEntryPoint? GetNear(Vector3 position)
        {
            int posMin = -1;
            float dist;
            float distTemp = Single.MaxValue;
            for (int i = 0; i < _entryLines.Length; i++)
            {
                dist = math.distance(position, _entryLines[i].entryPoint.position);
                if (_entryLines[i].entryPoint != null && dist < distTemp)
                {
                    posMin = i;
                    distTemp = dist;
                }
            }
            if(posMin !=-1)
                return _entryLines[posMin];
            
            return null;
        }
    }
}
