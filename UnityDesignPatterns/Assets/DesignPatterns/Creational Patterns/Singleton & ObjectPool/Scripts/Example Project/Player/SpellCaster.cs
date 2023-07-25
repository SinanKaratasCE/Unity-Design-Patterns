using System;
using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    // This is an example client that uses our simple object pool.
    public class SpellCaster : MonoBehaviour
    {
        [Tooltip("Projectile force")] [SerializeField]
        float muzzleVelocity = 2000;

        [Tooltip("End point of gun where shots appear")] [SerializeField]
        private Transform muzzlePosition;

        [Tooltip("Time between shots / smaller = higher rate of fire")] [SerializeField]
        float cooldownWindow = 0.8f;

        [Header("Object Pooling")] [SerializeField]
        private GameObject spellPrefab;

        public Animator playerAnimator;


        private ObjectPool<PoolObject> spellPool;
        [HideInInspector] public GameObject spell;

        public float nextTimeToShoot;

        private void OnEnable()
        {
            spellPool = new ObjectPool<PoolObject>(spellPrefab);
        }


        public virtual void FixedUpdate()
        {
            // shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot)
            {
                playerAnimator.SetBool("Attack", true);
            }
        }

        public virtual void CastSpell()
        {
            // get a pooled object instead of instantiating
            spell = spellPool.PullGameObject(muzzlePosition.position, muzzlePosition.rotation);
            ResetProjectileFeatures();

            if (spell == null)
                return;

            spell.SetActive(true);

            // align to gun barrel/muzzle position
            spell.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            // move projectile forward
            spell.GetComponent<Rigidbody>().AddForce(spell.transform.forward * muzzleVelocity,
                ForceMode.Acceleration);

            // turn off after a few seconds
            ProjectileBase projectileBase = spell.GetComponent<ProjectileBase>();
            projectileBase?.Deactivate();

            // set cooldown delay
            nextTimeToShoot = Time.time + cooldownWindow;
        }

        private void ResetProjectileFeatures()
        {
            var rBody = spell.GetComponent<Rigidbody>();
            rBody.velocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);
        }
    }
}