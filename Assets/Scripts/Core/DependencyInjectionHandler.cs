using UnicoStudio.GridSystem;
using UnityEngine;
using Zenject;

public class DependencyInjectionHandler : MonoInstaller
{
    [SerializeField] GridManager gridManager;
    [SerializeField] Camera currentCamera;

    public override void InstallBindings()
    {
        Container.Bind<GridManager>().FromInstance(gridManager).AsSingle();
        Container.Bind<Camera>().FromInstance(currentCamera).AsSingle();
    }
    

    
    
}