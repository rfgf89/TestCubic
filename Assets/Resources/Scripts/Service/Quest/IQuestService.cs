using System;

namespace PathGame.Service.Quest
{
    public interface IQuestService
    {
        public Quest GetQuest<T>(int index);
        public Quest[] GetQuest<T>();
        public Quest Registration<T>(string name);
        public void RemoveQuest<T>(int index);
        public void RemoveQuest(Type type,int index);
    }
}