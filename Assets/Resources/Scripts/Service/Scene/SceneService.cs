using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathGame.Service.Scene
{
        public class SceneService : MonoBehaviour, ISceneService
    {
        private readonly List<SceneServiceData> _states = new();

        [SerializeField] private SceneServiceConfig _scenesConfig;
#if UNITY_EDITOR
        [ReadOnly] [SerializeField] private List<SceneServiceData> _statesEditor = new();
#endif

        private void Awake()
        {
            _scenesConfig.Refresh();
            DontDestroyOnLoad(gameObject);
            _states.Add(new SceneServiceData(0, SceneServiceState.Current, SceneManager.GetActiveScene().name));
#if UNITY_EDITOR
            _statesEditor = _states;
#endif
        }

        private void ChangeScene(SceneServiceData sceneServiceData)
        {
            var nextScene = _scenesConfig.GetSceneBySceneState(sceneServiceData.GetSceneState(), sceneServiceData.GetLevelNumber()) ??
                            sceneServiceData;

            if (_states.Count != 0 && sceneServiceData.GetSceneState() == GetLastSceneData().GetSceneState()) return;

            var pos = _states.FindIndex(x => x.GetSceneState() == sceneServiceData.GetSceneState());
            if (pos != -1)
                for (var i = 0; i < _states.Count - pos - 1; i++)
                    _states.RemoveAt(_states.Count - 1);

            SceneManager.LoadScene(nextScene.GetSceneName());
            _states.Add(nextScene);
        }

        public void ToScene(SceneServiceData sceneServiceData)
        {
            switch (sceneServiceData.GetSceneState())
            {
                case SceneServiceState.ToBack:
                    BackScene();
                    return;
                case SceneServiceState.ToExit:
                    ExitGame();
                    return;
                case SceneServiceState.ToPause:
                    Pause();
                    return;
                case SceneServiceState.ToUnPause:
                    UnPause();
                    return;
                default:
                    ChangeScene(sceneServiceData);
                    return;
            }
        }

        public void ToScene(SceneServiceState sceneServiceState)
        {
            ToScene(_scenesConfig.GetSceneBySceneState(sceneServiceState));
        }

        public SceneServiceData GetLastSceneData()
        {
            return _states[^1];
        }


        private void BackScene()
        {
            if (_states.Count == 0) return;
            _states.RemoveAt(_states.Count - 1);

            if (_states.Count == 0) return;
            SceneManager.LoadScene(GetLastSceneData().GetSceneName());
        }

        private void Pause()
        {
            Time.timeScale = 0f;
        }

        private void UnPause()
        {
            Time.timeScale = 1f;
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }

}