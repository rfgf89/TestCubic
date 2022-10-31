using PathGame.Data;
using UnityEngine;

namespace PathGame.Interface
{
    public interface IPath
    {
        
        public ListEntryPoint? GetNear(Vector3 position);
    }
}