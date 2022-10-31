using System.Collections.Generic;
using System.Linq;
using PathGame.Data;
using UnityEngine;

namespace PathGame.Service.Scene
{
    [CreateAssetMenu(fileName = "SceneServiceConfig", menuName = "PathGame/Scene Service Config", order = 1)]
    public class SceneServiceConfig : ScriptableObject
    {

        [SerializeField]private Pair<SceneServiceState, SceneServiceData[]>[] _listSceneState;
        private readonly Dictionary<SceneServiceState, SceneServiceData[]> _dicSceneState = new Dictionary<SceneServiceState, SceneServiceData[]>();
        
        public void Refresh()
        {
            _dicSceneState.Clear();
            foreach (var val in _listSceneState)
                _dicSceneState.Add(val.firstValue, val.secondValue);
        }

        public SceneServiceData GetSceneBySceneState(SceneServiceState sceneServiceEvent, int index = 0)
        {
            if (_dicSceneState.TryGetValue(sceneServiceEvent, out var sceneDats))
                return sceneDats.First(x => x.GetLevelNumber() == index);
  
            return null;
        }
        
    }
}