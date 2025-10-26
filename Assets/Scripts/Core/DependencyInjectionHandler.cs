using UnicoStudio.GridSystem;
using UnityEngine;
using Zenject;


namespace UnicoStudio.Core
{
    public class DependencyInjectionHandler : MonoInstaller
    {
        [SerializeField] GridManager gridManager;
        [SerializeField] Camera currentCamera;

        public override void InstallBindings()
        {
            Container.Bind<GridManager>().FromInstance(gridManager).AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(currentCamera).AsSingle().NonLazy();
        }
    }
    
    
}