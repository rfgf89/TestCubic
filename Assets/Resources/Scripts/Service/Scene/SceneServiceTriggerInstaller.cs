using PathGame.Service.Scene;
using UnityEngine;
using Zenject;

namespace PathGame.Installer
{
    public class SceneServiceTriggerInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private SceneServiceTrigger[] _sceneTriggers;

        public override void InstallBindings()
        {
            BindSelf();
        }

        private void BindSelf()
        {
            Container
                .BindInterfacesTo<SceneServiceTriggerInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        public void Initialize()
        {
            foreach (var trigger in _sceneTriggers)
                Container.Inject(trigger);

        }
    }
}