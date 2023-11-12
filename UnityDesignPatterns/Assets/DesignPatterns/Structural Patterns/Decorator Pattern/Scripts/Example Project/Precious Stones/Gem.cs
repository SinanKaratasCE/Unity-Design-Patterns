using UnityEngine;

namespace DesignPatterns.Decorator
{

    public class Gem : PreciousStone
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