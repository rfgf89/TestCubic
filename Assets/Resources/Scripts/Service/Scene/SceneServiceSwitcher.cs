using PathGame.Interface;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace PathGame.Service.Scene
{
    public class SceneServiceSwitcher : MonoBehaviour, ITrigger
    {
        [FormerlySerializedAs("_sceneData")] [SerializeField, HideLabel] private SceneServiceData sceneServiceData;
        
        private ISceneService _sceneService;

        [Inject]
        private void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public void Execute() => _sceneService.ToScene(sceneServiceData);
    }
}