using System;
using DesignPatterns.SingletonObjectPool;
using UnityEngine;

namespace DesignPatterns.Strategy
{
    public class FireWand : SpellCaster
    {
        [SerializeField] private WandChanger wandChanger;

        public override void FixedUpdate()
        {
            // shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot)
            {
                playerAnimator.SetBool("Attack", true);
                wandChanger.canChange = false;
            }
        }

        public override void CastSpell()
        {
            base.CastSpell();
            spell.GetComponent<SpellBase>().SetDamageType(new FireDamage());
        }
    }
}