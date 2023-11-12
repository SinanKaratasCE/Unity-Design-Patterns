using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class LightGloves : StatusEffectDecorator
    {
        public LightGloves(StatusEffect statusEffect, Item itemInfo)
        {
            this.statusEffect = statusEffect;
            item = itemInfo;
        }

        public override float DamageEffect()
        {
            return statusEffect.DamageEffect() + item.damageValue;
        }

        public override float DiggingSpeedEffect()
        {
            return statusEffect.DiggingSpeedEffect() + item.diggingSpeedValue;
        }

        public override float MovementSpeedEffect()
        {
            return statusEffect.MovementSpeedEffect() + item.movementSpeedValue;
        }

        public override float EfficiencyEffect()
        {
            return statusEffect.EfficiencyEffect() + item.efficiencyValue;
        }
    }
}