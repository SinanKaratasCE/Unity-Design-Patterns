using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class Rock : PreciousStone
    {
        public override int GetPrize()
        {
            return preciousStoneFeatures.prize;
        }
        
        public override int GetDurability()
        {
            return preciousStoneFeatures.durability;
        }
        
        public override string GetName()
        {
            return preciousStoneFeatures.name;
        }
    }
}   