using UnityEngine;

namespace PathGame.Interface
{
    public interface ICellItemMove
    {
        public Transform transform { get; }
        public Bounds cellBounds { get; }
    }
}