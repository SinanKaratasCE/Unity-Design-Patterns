using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Factory
{
    // a common interface between weapons
    public interface IWeapon
    {
        // add common properties and methods here
        public string WeaponName { get; set; }
        public int Damage { get; set; }
        public int Durability { get; set; }
        public Sprite Icon { get; set; }


        // customize this for each concrete product
        public void Initialize();
    }
}