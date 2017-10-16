using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IPoolGameController>()
                 .FromComponentInHierarchy()
                 .AsSingle()
                 .NonLazy();

        Container.Bind<IGameObjectState>().To<GameStates.WaitingForStrikeState>().AsSingle().NonLazy();
    }
}