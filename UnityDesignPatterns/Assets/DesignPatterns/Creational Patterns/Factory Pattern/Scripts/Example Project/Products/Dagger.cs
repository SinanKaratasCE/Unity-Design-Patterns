using UnityEngine;

namespace DesignPatterns.Factory
{

    public class Dagger : MonoBehaviour,IWeapon
    {
        [SerializeField] private WeaponStats weaponStats;
        public string WeaponName { get; set; }
        public int Damage { get; set; }
        public int Durability { get; set; }
        public Sprite Icon { get; set; }
        public void Initialize()
        {
            WeaponName = weaponStats.weaponName;
            Damage = weaponStats.damage;
            Durability = weaponStats.durability;
            Icon = weaponStats.icon;
            
            //Dagger specific initialization logic here
            //Debug.Log($"Stab with {WeaponName} to attack.");
        }
    }

}