using UnityEngine;

namespace DesignPatterns.Decorator
{

    public enum ItemName
    {
        EfficiencyStones = 0,
        DiamondCoating = 1,
        LightGloves = 2,
        SpeedyBoots = 3,
    }
    
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class Item : ScriptableObject
    {
            
        public ItemName itemName;
        public int damageValue;
        public float diggingSpeedValue;
        public int movementSpeedValue;
        public float efficiencyValue;
        public int price;       
 
    }

}