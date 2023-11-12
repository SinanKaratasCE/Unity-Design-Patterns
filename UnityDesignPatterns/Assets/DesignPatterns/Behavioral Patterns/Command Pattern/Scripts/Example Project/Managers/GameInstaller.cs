using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DesignPatterns.Command
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMover playerMover;

        [SerializeField] private GameManager gameManager;


        public override void InstallBindings()
        {
            Container.BindInstance(playerMover);
            Container.BindInstance(gameManager);
        }
    }
}