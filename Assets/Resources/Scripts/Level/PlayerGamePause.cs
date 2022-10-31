using PathGame.Input;
using PathGame.Interface;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerGamePause : MonoBehaviour
{
    [SerializeField] private UnityEvent _pauseEvent;
    [SerializeField] private UnityEvent _unPauseEvent;

    private IPlayerInput _playerInput;
    private float _timeScale;

    [Inject]
    private void Construct(IPlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void Start()
    {
        _playerInput.AddListener(PathGamePlayerInput.InputType.GamePause, Pause, null, null);
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        if (Time.timeScale > 0f)
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0f;
            _pauseEvent?.Invoke();
        }
        else
        {
            Time.timeScale = _timeScale;
            _unPauseEvent?.Invoke();
        }
    }
}
