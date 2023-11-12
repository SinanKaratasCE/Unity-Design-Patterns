using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DesignPatterns.SingletonObjectPool
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnDelay = 1f;
        public int totalSpawnCount = 30;
        [SerializeField] private float spawnDistance = 5f;
        [SerializeField] private GameObject player;
        private int currentSpawnCount = 0;
        public bool canSpawn = true;
        private List<GameObject> enemies = new List<GameObject>();

        private ObjectPool<PoolObject> enemyPool;

        private void Start()
        {
            SingletonContainer.Instance.GameManager._restartGame += Reset;
            enemyPool = new ObjectPool<PoolObject>(enemyPrefab);
            StartSpawn();
        }

        private void OnDisable()
        {
            SingletonContainer.Instance.GameManager._restartGame -= Reset;
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

                if (currentSpawnCount >= totalSpawnCount)
                {
                    canSpawn = false;
                }
            }
        }

        public void Spawn()
        {
            // Calculate a random position within away from the player
            currentSpawnCount++;
            var enemy = enemyPool.PullGameObject(CalculateSpawnPosition(), Random.rotation);
            enemies.Add(enemy);
            ResetEnemyFeatures(enemy);
        }

        private void ResetEnemyFeatures(GameObject enemy)
        {
            var rBody = enemy.GetComponent<Rigidbody>();
            rBody.velocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);
        }

        private Vector3 CalculateSpawnPosition()
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 spawnPosition =
                player.transform.position + new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * spawnDistance;
            return spawnPosition;
        }

        public void Reset()
        {
            currentSpawnCount = 0;
            canSpawn = true;
            StartSpawn();
        }

        public void DeactivateAllEnemies()
        {
            foreach (var enemy in enemies)
            {
                enemy.SetActive(false);
            }

            canSpawn = false;
        }
    }
}