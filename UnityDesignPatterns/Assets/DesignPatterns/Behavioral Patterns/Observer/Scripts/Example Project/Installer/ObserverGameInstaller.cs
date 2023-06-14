using UnityEngine;
using DesignPatterns.Observer;
using Zenject;


public class ObserverGameInstaller : MonoInstaller
{
    [SerializeField] private SuccessSubject successSubject;
    [SerializeField] private FailSubject failSubject;
    [SerializeField] private PlayerBase player;

    public override void InstallBindings()
    {
        Container.BindInstance(successSubject);
        Container.BindInstance(failSubject);
        Container.BindInstance(player);

    }
}