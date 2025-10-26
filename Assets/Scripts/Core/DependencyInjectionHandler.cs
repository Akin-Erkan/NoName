using UnicoStudio.Enemy;
using UnicoStudio.GridSystem;
using UnicoStudio.Unit;
using UnityEngine;
using Zenject;


namespace UnicoStudio.Core
{
    public class DependencyInjectionHandler : MonoInstaller
    {
        [SerializeField] GridManager gridManager;
        [SerializeField] Camera currentCamera;
        [SerializeField] EnemySpawnController enemySpawnController;
        [SerializeField] UnitTargetingSystem unitTargetingSystem;

        public override void InstallBindings()
        {
            Container.Bind<GridManager>().FromInstance(gridManager).AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(currentCamera).AsSingle().NonLazy();
            Container.Bind<EnemySpawnController>().FromInstance(enemySpawnController).AsSingle().NonLazy();
            Container.Bind<UnitTargetingSystem>().FromInstance(unitTargetingSystem).AsSingle().NonLazy();
        }
    }
    
    
}