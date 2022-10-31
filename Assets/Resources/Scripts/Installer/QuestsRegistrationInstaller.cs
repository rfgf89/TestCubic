using PathGame.Factory;
using PathGame.Interface;
using PathGame.Level;
using PathGame.Service.Coin;
using PathGame.Service.Quest;
using UnityEngine;
using Zenject;

public class QuestsRegistrationInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private GeneralQuestFactory _questFactory;
    [SerializeField] private IntTextQuestView _viewEntryPointQuest;
    public override void InstallBindings()
    {
        BindQuestFactory();
        BindSelf();
    }
    private void BindQuestFactory()
    {
        Container
            .Bind<IQuestFactory>()
            .To<GeneralQuestFactory>()
            .FromInstance(_questFactory)
            .AsSingle();
    }
    private void BindSelf()
    {
        Container
            .BindInterfacesTo<QuestsRegistrationInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    public void Initialize()
    {
        var questService = Container.Resolve<IQuestService>();
        var coinService = Container.Resolve<ICoinService>();
        
        var quest = questService
            .Registration<EntryQuestHandler>("Complete Path")
            .Init(0.5f);
        
        quest.questComplete += () => coinService.AddCoin(Random.Range(5, 10));
        
        _viewEntryPointQuest.Init(quest);

    }
}
