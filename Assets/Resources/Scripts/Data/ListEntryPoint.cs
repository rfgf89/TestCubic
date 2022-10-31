using System;
using PathGame.Enum;
using UnityEngine;

namespace PathGame.Data
{
    [Serializable]
    public class ListEntryPoint
    {
        public Vector3 center;
        public Transform entryPoint;
        public Transform[] lines;
        public event Action<PathState> pathOpen;

        public void PathChangeState(PathState state)
        {
            pathOpen?.Invoke(state);
        }
    }
}