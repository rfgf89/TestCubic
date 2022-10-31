using PathGame.Enum;
using PathGame.Interface;
using UnityEngine;

namespace PathGame.Level
{
    [RequireComponent(typeof(MeshRenderer))]
    public class VisualPathSegment : MonoBehaviour, IPathState
    {

        [SerializeField] private Color _closeColor;
        [SerializeField] private Color _openColor;

        private MeshRenderer _meshRenderer;

        public void ChangeState(PathState state)
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();

            if (_meshRenderer.material != null)
                _meshRenderer.material.color = state == PathState.Open ? _openColor : _closeColor;

        }
    }
}
