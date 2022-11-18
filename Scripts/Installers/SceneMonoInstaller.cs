using UnityEngine;
using Zenject;

public class SceneMonoInstaller : MonoInstaller
{
    public CoroutineHelper coroutineHelper;
    public override void InstallBindings()
    {
        Container.Bind<BoardManager>().AsSingle();
        Container.Bind<BoardActionsManager>().AsSingle();
        Container.Bind<BossManager>().AsSingle();
        Container.Bind<BoardShuffleUtility>().AsSingle();
        Container.Bind<ColorChoiceUtility>().AsSingle();
        Container.Bind<InputHandler>().AsSingle();
        Container.Bind<FiguresWithdrawManager>().AsSingle();
        Container.Bind<LevelManager>().AsSingle();
        Container.Bind<FigureCreationManager>().AsSingle();
        Container.Bind<FigurePlacementManager>().AsSingle();
        Container.Bind<StepManager>().AsSingle();
        Container.BindInstance(coroutineHelper);
    }
    public override void Start()
    {
        base.Start();
        Container.Resolve<InputHandler>();
        var levelManager = Container.Resolve<LevelManager>();
        levelManager.StartLevel();
    }
}