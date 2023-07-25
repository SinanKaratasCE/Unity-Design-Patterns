using UnityEngine;
using Zenject;


namespace DesignPatterns.Observer
{
    public class GameInstaller : MonoInstaller
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
}