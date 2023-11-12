using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;
using System.Collections.Generic;

namespace DesignPatterns.Flyweight
{
    public class EnemySpawner : MonoBehaviour
    {
        private const float MinSpawnArea = -25f;
        private const float MaxSpawnArea = 25f;
        private float _spawnRate = 0.5f;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyCount = 10;
        private List<GameObject> _enemies = new List<GameObject>();
        private FlyweightGameManager _flyweightGameManager;

        [Inject]
        private void OnInstaller(FlyweightGameManager flyweightGameManager)
        {
            _flyweightGameManager = flyweightGameManager;
        }

        private void Start()
        {
            SpawnAllEnemies();
            DisableAllEnemies();
            Initializer();
        }

        private void SpawnAllEnemies()
        {
            for (var i = 0; i < enemyCount; i++)
            {
                GameObject instantiatedEnemy = Instantiate(enemyPrefab,
                    new Vector3(Random.Range(MinSpawnArea, MaxSpawnArea), 0, Random.Range(MinSpawnArea, MaxSpawnArea)),
                    Quaternion.identity);

                instantiatedEnemy.GetComponent<Enemy>().SetFlyweightGameManager(_flyweightGameManager);
                _enemies.Add(instantiatedEnemy);
            }
        }

        public void Initializer()
        {
            StartCoroutine(ActivateAllEnemies());
            _flyweightGameManager.EnemyCount = enemyCount;
            _flyweightGameManager.RemainingTime = enemyCount * 1.3f;
        }

        private IEnumerator ActivateAllEnemies()
        {
            for (var i = 0; i < enemyCount; i++)
            {
                _enemies[i].SetActive(true);
                yield return new WaitForSeconds(_spawnRate);
            }
        }

        public void DisableAllEnemies()
        {
            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].SetActive(false);
            }
        }
    }
}