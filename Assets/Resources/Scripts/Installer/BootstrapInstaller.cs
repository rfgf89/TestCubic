using PathGame.Factory;
using PathGame.Service.Coin;
using PathGame.Service.Quest;
using PathGame.Service.Scene;
using UnityEngine;
using Zenject;

namespace PathGame.Installer
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneService _sceneService;
        [SerializeField] private CoinService _coinService;
        [SerializeField] private QuestService _questService;

        public override void InstallBindings()
        {
            BindQuestService();
            BindCoinService();
            BindSceneService();
        }

        private void BindQuestService()
        {
            Container
                .Bind<IQuestService>()
                .To<QuestService>()
                .FromInstance(_questService)
                .AsSingle();
        }

        private void BindCoinService()
        {
            Container
                .Bind<ICoinService>().
                To<CoinService>()
                .FromInstance(_coinService)
                .AsSingle();
        }

        private void BindSceneService()
        {
            Container.Bind<ISceneService>()
                .To<SceneService>()
                .FromComponentInNewPrefab(_sceneService)
                .AsSingle();
        }
        
    }
}