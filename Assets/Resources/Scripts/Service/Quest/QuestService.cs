using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PathGame.Service.Quest
{
    public class QuestService : MonoBehaviour, IQuestService
    {
        private readonly Dictionary<Type, Dictionary<int, Quest>> _allQuests = new Dictionary<Type, Dictionary<int, Quest>>();
        private int _questIndex = 0;
        
        
        public Quest Registration<T>(string name)
        {
            
            int index = _questIndex++;
            Quest quest = new Quest(name,typeof(T), index, this);
            
            if (_allQuests.TryGetValue(typeof(T), out var value))
                value.Add(index, quest);
            else
            {
                var dicIndex = new Dictionary<int, Quest>();
                _allQuests.Add(typeof(T), dicIndex);
                dicIndex.Add(index, quest);
            }
            
            return quest;
        }

        public void RemoveQuest<T>(int index)
        {
            if(_allQuests.TryGetValue(typeof(T), out var allQuests))
                if (allQuests.ContainsKey(index))
                    allQuests.Remove(index);
        }

        public void RemoveQuest(Type type, int index)
        {
            if(_allQuests.TryGetValue(type, out var allQuests))
                if (allQuests.ContainsKey(index))
                    allQuests.Remove(index);
        }

        public Quest GetQuest<T>(int index)
        {
            if (_allQuests.TryGetValue(typeof(T), out var allQuests))
                if (allQuests.TryGetValue(index, out var indexQuest))
                    return indexQuest;
            
            return null;
        }
        public Quest[] GetQuest<T>()
        {
            if (_allQuests.TryGetValue(typeof(T), out var allQuests))
                return allQuests.Values.ToArray();
            
            return Array.Empty<Quest>();
        }
    }
}

