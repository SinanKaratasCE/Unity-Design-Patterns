using UnityEngine;

namespace DesignPatterns.SingletonObjectPoolOtherVersion
{
    // This is an example client that uses our simple object pool.
    public class SpellCasterOtherVersion : MonoBehaviour
    {
        [Tooltip("Projectile force")] [SerializeField]
        float muzzleVelocity = 2000;

        [Tooltip("End point of gun where shots appear")] [SerializeField]
        private Transform muzzlePosition;

        [Tooltip("Time between shots / smaller = higher rate of fire")] [SerializeField]
        float cooldownWindow = 0.8f;

        [Tooltip("Reference to Object Pool")] [SerializeField]
        private ObjectPool objectPool;

        private float nextTimeToShoot;
        private Animator playerAnimator;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            // shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot && objectPool != null)
            {
                playerAnimator.SetBool("Attack", true);
            }
        }

        private void CastSpell()
        {
            // get a pooled object instead of instantiating
            GameObject spell = objectPool.GetPooledObject().gameObject;

            if (spell == null)
                return;

            spell.SetActive(true);

            // align to gun barrel/muzzle position
            spell.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            // move projectile forward
            spell.GetComponent<Rigidbody>().AddForce(spell.transform.forward * muzzleVelocity,
                ForceMode.Acceleration);

            // turn off after a few seconds
            ProjectileOtherVersion projectile = spell.GetComponent<ProjectileOtherVersion>();
            projectile?.Deactivate();

            // set cooldown delay
            nextTimeToShoot = Time.time + cooldownWindow;
        }
    }
}