using UnityEngine;

namespace DesignPatterns.Factory
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 1)]
    public class WeaponStats : ScriptableObject
    {
        public string weaponName;
        public int damage;
        public int durability;
        public Sprite icon;
    }
}