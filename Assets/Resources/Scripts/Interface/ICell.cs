using PathGame.Level;
using UnityEngine;

namespace PathGame.Interface
{
    public interface ICell
    {
        public bool Contains(Vector3 position);
        public void SetItem(CellValueBehaviour cellObject);
        public GameObject GetItem();
        public int Index { get; set; }
        public Vector3 GetSizeCell();
        public bool CellEmpty();

        public GameObject gameObject { get; }
        
    }
}