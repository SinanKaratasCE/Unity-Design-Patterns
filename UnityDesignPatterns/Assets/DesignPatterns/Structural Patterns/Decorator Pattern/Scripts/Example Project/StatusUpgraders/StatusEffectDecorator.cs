using UnityEngine;

namespace DesignPatterns.Decorator
{

    public abstract class StatusEffectDecorator : StatusEffect
    {
        public StatusEffect statusEffect;
        public Item item;
    }

}