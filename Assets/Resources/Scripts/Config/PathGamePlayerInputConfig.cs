using PathGame.Interface;
using PathGame.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PathGame.Config
{
    [CreateAssetMenu(fileName = "PathGamePlayerInputConfig", menuName = "PathGame/Path Game Player Input Config", order = 0)]
    public class PathGamePlayerInputConfig : ScriptableObject, IPlayerInputConfig
    {
        [SerializeField, HideLabel] private PathGamePlayerInput _playerInput;
        public IPlayerInput GetPlayerInput() => _playerInput.Refresh();
        
    }
}
