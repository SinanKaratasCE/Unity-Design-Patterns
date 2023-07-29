using System;
using UnityEngine;

namespace DesignPatterns.Factory
{
    // base class for factories
    public abstract class Factory : MonoBehaviour
    {
        public abstract IWeapon GetWeapon(Vector3 position);
        public abstract FactoryType FactoryType { get; set; }
        public abstract float ProductionTime { get; set; }

        // shared method with all factories
        public string GetLog(IWeapon weapon)
        {
            string logMessage = "Factory: created weapon " + weapon.WeaponName;
            return logMessage;
        }
    }
}