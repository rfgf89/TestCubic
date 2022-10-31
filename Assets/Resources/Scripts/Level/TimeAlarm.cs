using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class TimeAlarm : MonoBehaviour
{
    [SerializeField] private float _timeInSecond;
    [SerializeField] private bool _IsStart;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private UnityEvent _endAction;

    private float _currentTimeInSecond;

    private void Start()
    {
        if (_IsStart)
            StartAlarm();
    }

    private void FixedUpdate()
    {
        if (_IsStart)
        {
            _currentTimeInSecond -= Time.fixedDeltaTime;
            if(_text!=null)
                _text.text = math.floor(_currentTimeInSecond/60f) + " : " + math.floor(_currentTimeInSecond % 60f);
            
            if (_currentTimeInSecond <= 0f)
            {
                _IsStart = false;
                _endAction?.Invoke();
                _currentTimeInSecond = 0f;
            }
        }
        
    }
    private void StartAlarm()
    {
        _currentTimeInSecond = _timeInSecond;
        _IsStart = true;
    }

    public void StopAlarm()
    {
        _IsStart = false;
    }
}
