using System;
using System.Collections.Generic;
using PathGame.Interface;
using PathGame.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PathGame.Input
{
    [Serializable]
    public class PathGamePlayerInput : IPlayerInput
    {
        [SerializeField] private List<Pair<InputType, InputAction>> _allInputs;
        private Dictionary<InputType, InputAction> _allInputsDictionary;
        
        public IPlayerInput Refresh()
        {
            if (_allInputs != null)
            {
                _allInputsDictionary = new Dictionary<InputType, InputAction>(_allInputs.Count);

                foreach (var val in _allInputs)
                {
                    val.secondValue.Enable();
                    _allInputsDictionary.Add(val.firstValue, val.secondValue);
                }
            }

            return this;
        }
        
        public enum InputType
        {
            SelectorClick,
            SelectorPosition,
            GamePause,
        }    
            
        public void AddListener(System.Enum type, 
            Action<InputAction.CallbackContext> start,
            Action<InputAction.CallbackContext> perform, 
            Action<InputAction.CallbackContext> cancel)
        {
            if (_allInputsDictionary.TryGetValue((InputType)type, out var eventInput))
            {
                if(start!=null)
                    eventInput.started += start;
                if(perform!=null)
                    eventInput.performed += perform;
                if(cancel!=null)
                    eventInput.canceled += cancel;
                
                return;
            }
            Debug.LogError("Not Input in PlayerInput");

        }

        public void RemoveListener(System.Enum type,
            Action<InputAction.CallbackContext> start,
            Action<InputAction.CallbackContext> perform, 
            Action<InputAction.CallbackContext> cancel)
        {
            if (_allInputsDictionary.TryGetValue((InputType)type, out var eventInput))
            {
                if(start!=null)
                    eventInput.started -= start;
                if(perform!=null)
                    eventInput.performed -= perform;
                if(cancel!=null)
                    eventInput.canceled -= cancel;
                return;
            }
            
            Debug.LogError("Not Input in PlayerInput");
        }
        

    }

}

