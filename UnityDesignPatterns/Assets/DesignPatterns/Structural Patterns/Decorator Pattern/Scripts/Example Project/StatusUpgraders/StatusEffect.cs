using UnityEngine;

namespace DesignPatterns.Decorator
{
    public abstract class StatusEffect
    {
        public abstract float DamageEffect();
        public abstract float DiggingSpeedEffect();
        public abstract float MovementSpeedEffect();
        public abstract float EfficiencyEffect();
    }
}