using System;
using System.Runtime.CompilerServices;

namespace PathGame.Service.Quest
{
    public class Quest
    {
        private float _difficulty = 1.0f;
        private float _progress;
        private readonly Type _type;
        private readonly int _index;
        private readonly IQuestService _service;
        private string _name;
        
        public event Action questComplete;
        public event Action questUpdate;
        
        public Quest(string name,Type type, int index, IQuestService service)
        {
            _name = name;
            _type = type;
            _index = index;
            _service = service;
        }
        
        public Quest Init(float difficulty)
        {
            _difficulty = difficulty;
            _progress = 0f;

            return this;
        }
        
        public Quest Up()
        {
            _progress += _difficulty;
            questUpdate?.Invoke();

            if (_progress >= 1f)
            {
                questComplete?.Invoke();
                _service.RemoveQuest(_type, _index);
            }

            return this;
        }
        public Quest Down()
        {
            _progress -= _difficulty;
            if (_progress < 0f)
                _progress = 0f;
            
            questUpdate?.Invoke();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get() => (int)(_progress / _difficulty);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetMax() => (int)(1f / _difficulty);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetName() => _name;
    }
}