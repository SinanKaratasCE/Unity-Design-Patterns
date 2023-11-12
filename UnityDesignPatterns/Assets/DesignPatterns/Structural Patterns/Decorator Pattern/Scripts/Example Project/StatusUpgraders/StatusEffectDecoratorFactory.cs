using UnityEngine;

namespace DesignPatterns.Decorator
{

    public static class StatusEffectDecoratorFactory
    {

        public static StatusEffect CreateStatusEffectDecorator(Item item, StatusEffect aStatusEffect)
        {
            switch(item.itemName) {
                case ItemName.DiamondCoating:      return new DiamondCoating(aStatusEffect,item);
                case ItemName.SpeedyBoots:   return new SpeedyBoots(aStatusEffect,item); 
                case ItemName.LightGloves:   return new LightGloves(aStatusEffect,item); 
                case ItemName.EfficiencyStones:   return new EfficiencyStones(aStatusEffect,item); 
                default:                return null;
            }
        }
 
    }

}