using System;
using UnityEngine.InputSystem;

namespace PathGame.Interface
{
    public interface IPlayerInput
    {
        public void AddListener(System.Enum type,
            Action<InputAction.CallbackContext> start,
            Action<InputAction.CallbackContext> perform, 
            Action<InputAction.CallbackContext> cancel);
        public void RemoveListener(System.Enum type,
            Action<InputAction.CallbackContext> start,
            Action<InputAction.CallbackContext> perform, 
            Action<InputAction.CallbackContext> cancel);
        public IPlayerInput Refresh();
    }
}