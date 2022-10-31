using PathGame.Config;
using PathGame.Factory;
using PathGame.Interface;
using PathGame.Level;
using PathGame.Player;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private PathGamePlayerInputConfig _playerInput;
    [SerializeField] private PlayerInputController _playerInputController;
    [SerializeField] private GridField2D _gridField;
    [SerializeField] private PlayerFieldSelector _fieldSelector;
    [SerializeField] private PlayerGamePause _playerGamePause;

    public override void InstallBindings()
    {
        BindGridFied();
        BindSelf();
        BindPlayerInput();
    }



    private void BindGridFied()
    {
        Container
            .Bind<IGridField>()
            .To<GridField2D>()
            .FromInstance(_gridField)
            .AsSingle();
    }

    private void BindSelf()
    {
        Container
            .BindInterfacesTo<LevelInstaller>()
            .FromInstance(this)
            .AsSingle();
    }

    private void BindPlayerInput()
    {
        Container
            .Bind<IPlayerInput>()
            .FromInstance(_playerInput
                .GetPlayerInput());
    }
    

    public void Initialize()
    {
        Container.Inject(_playerInputController);
        Container.Inject(_fieldSelector);
        Container.Inject(_playerGamePause);
        
        var gridField = Container.Resolve<IGridField>();

        for (int i = 0; i < gridField.Count; i++)
        {
            Container.Inject(gridField.GetCell(i));
        }

    }
}
