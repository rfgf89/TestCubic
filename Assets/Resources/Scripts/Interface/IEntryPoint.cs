using System;
using System.Collections.Generic;
using PathGame.Data;

namespace PathGame.Interface
{
    public interface IEntryPoint
    {
        public string Tag { get; }
        public List<ListEntryPoint> PathFind();
        public void SetPath(List<ListEntryPoint> path);
        public bool isPathFinding { get; }
        public event Action<List<ListEntryPoint>, bool> pathUpdate;
    }
}