using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class PlayerBaseStatus : StatusEffect
    {
        public override float DamageEffect()
        {
            return 10;
        }

        public override float DiggingSpeedEffect()
        {
            return 1;
        }

        public override float MovementSpeedEffect()
        {
            return 5;
        }

        public override float EfficiencyEffect()
        {
            return 1;
        }
    }
}