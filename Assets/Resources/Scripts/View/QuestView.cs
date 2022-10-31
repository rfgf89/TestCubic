using PathGame.Service.Quest;
using UnityEngine;

namespace PathGame.Level
{
    public class QuestView : MonoBehaviour
    {
        protected Quest _quest;
        
        public QuestView Init(Quest quest)
        {
            _quest = quest;
            return this;
        }
    }
}