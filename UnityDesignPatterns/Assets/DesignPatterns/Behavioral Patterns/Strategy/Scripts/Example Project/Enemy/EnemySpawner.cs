using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.SingletonObjectPool;
using Zenject;

namespace DesignPatterns.Strategy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnDelay = 1f;
        [SerializeField] private Transform spawnPosition;
        public int totalSpawnCount = 30;
        private int _currentSpawnCount = 0;
        public bool canSpawn = true;
        private List<GameObject> _enemies = new List<GameObject>();
        private GameManager _gameManager;

        private ObjectPool<PoolObject> enemyPool;

        [Inject]
        private void OnInstaller(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            enemyPool = new ObjectPool<PoolObject>(enemyPrefab);
            StartSpawn();
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnOverTime());
        }


        IEnumerator SpawnOverTime()
        {
            while (canSpawn)
            {
                Spawn();
                yield return new WaitForSeconds(spawnDelay);

                if (_currentSpawnCount >= totalSpawnCount)
                {
                    canSpawn = false;
                }
            }
        }

        public void Spawn()
        {
            // Calculate a random position within away from the player
            _currentSpawnCount++;

            var enemy = enemyPool.PullGameObject(spawnPosition.position, Quaternion.identity);
            _enemies.Add(enemy);
            enemy.GetComponent<Enemy>().InjectGameManager(_gameManager);

            var rBody = enemy.GetComponent<Rigidbody>();
            rBody.velocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);
        }

        public void Reset()
        {
            _currentSpawnCount = 0;
            canSpawn = true;
            StartSpawn();
        }

        public void DeactivateAllEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.SetActive(false);
            }

            canSpawn = false;
        }
    }
}