using System.Collections;
using System.Collections.Generic;
using DesignPatterns.SingletonObjectPool;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class VFXSpawner : MonoBehaviour
    {
        [Header("Object Pooling")] [SerializeField]
        private GameObject vfxPrefab;
        private ObjectPool<PoolObject> vfxPool;
        [HideInInspector] public GameObject vfx;


        private void Start()
        {
            vfxPool = new ObjectPool<PoolObject>(vfxPrefab);
        }


        public GameObject Spawn(Vector3 spawnPosition)
        {
            vfx = vfxPool.PullGameObject(spawnPosition, Quaternion.identity);

            if (vfx == null)
                return null;

            vfx.SetActive(true);

            return vfx;
        }
    }
}