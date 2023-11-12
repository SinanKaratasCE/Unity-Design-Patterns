using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace DesignPatterns.Flyweight
{
    public class FlyweightGameInstaller : MonoInstaller
    {
        [SerializeField] private FlyweightGameManager flyweightGameManager;
        [SerializeField] private FlyweightUIManager flyweightUIManager;
        [SerializeField] private EnemySpawner enemySpawner;

        public override void InstallBindings()
        {
            Container.BindInstance(flyweightGameManager);
            Container.BindInstance(flyweightUIManager);
            Container.BindInstance(enemySpawner);
        }
    }
}