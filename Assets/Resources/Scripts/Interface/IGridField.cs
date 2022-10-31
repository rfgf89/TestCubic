using System;
using PathGame.Level;
using UnityEngine;

namespace PathGame.Interface
{
    public interface IGridField
    {
        public ICell GetCell(Vector3 position);
        public Vector3 GetCellSize();
        public int Count { get; }
        public GameObject GetCell(int index);
        
        public event Action<int> cellUpdate;
        public void CellUpdate(int index);
    }
}