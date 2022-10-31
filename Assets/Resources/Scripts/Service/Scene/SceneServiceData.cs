using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace PathGame.Service.Scene
{
    [Serializable]
    public class SceneServiceData
    {
        [SerializeField] private int _levelNumber;
        [FormerlySerializedAs("_sceneState")] [SerializeField] private SceneServiceState sceneServiceState;
        [SerializeField] private string _sceneName;

        public SceneServiceData(int levelNumber, SceneServiceState sceneServiceState, string sceneName)
        {
            _levelNumber = levelNumber;
            _sceneName = sceneName;
            this.sceneServiceState = sceneServiceState;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetLevelNumber()
        {
            return _levelNumber;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetSceneName()
        {
            return _sceneName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SceneServiceState GetSceneState()
        {
            return sceneServiceState;
        }

        public SceneServiceData SetSceneData(SceneServiceData sceneServiceData)
        {
            _levelNumber = sceneServiceData.GetLevelNumber();
            _sceneName = sceneServiceData.GetSceneName();
            sceneServiceState = sceneServiceData.GetSceneState();

            return this;
        }
    }
}
