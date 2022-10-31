using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PathGame.Level
{
    public class IntTextQuestView : QuestView
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private UnityEvent _eventComplete;
        
        private void Start()
        {
            _quest.questUpdate += QuestUpdate;
            _quest.questComplete += QuesComplete;
            QuestUpdate();
        }

        private void QuestUpdate() =>
            _text.text = _quest.GetName() + " " + _quest.Get() + " / " + _quest.GetMax();

        private void QuesComplete()
        {
            _eventComplete?.Invoke();
            _text.text = _quest.GetName() + " " + _quest.GetMax() + " / " + _quest.GetMax();
        }

        private void OnDestroy()
        {
            _quest.questUpdate -= QuestUpdate;
            _quest.questComplete -= QuesComplete;
        }
    }
}