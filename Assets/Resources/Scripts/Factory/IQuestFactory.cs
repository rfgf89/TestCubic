using PathGame.Factory;
using PathGame.Level;
using PathGame.Service.Quest;
using UnityEngine;

namespace PathGame.Interface
{
    public interface IQuestFactory
    {
        public QuestView Create(QuestViewType type, Quest quest, Vector3 at);
    }
}