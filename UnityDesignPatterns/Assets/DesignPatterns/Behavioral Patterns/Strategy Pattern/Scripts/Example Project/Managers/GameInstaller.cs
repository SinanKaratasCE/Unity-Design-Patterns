using UnityEngine;
using Zenject;

namespace DesignPatterns.Strategy
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIManager uiManager;

        [SerializeField] private GameManager gameManager;
        [SerializeField] private EnemySpawner enemySpawner;


        public override void InstallBindings()
        {
            Container.BindInstance(uiManager);
            Container.BindInstance(gameManager);
            Container.BindInstance(enemySpawner);
        }
    }
}