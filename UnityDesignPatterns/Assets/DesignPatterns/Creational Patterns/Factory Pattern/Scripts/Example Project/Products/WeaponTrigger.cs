using System;
using System.ComponentModel;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class WeaponTrigger : MonoBehaviour
    {
        private IWeapon _weapon;
        private WeaponMover _weaponMover;

        private void Start()
        {
            _weapon = GetComponent<IWeapon>();
            _weaponMover = GetComponent<WeaponMover>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ContainerShip ship))
            {
                _weaponMover.enabled = false;
                ship.AddWeaponToContainerShip(_weapon);
                ship.LoadWeaponsIntoProperLocations(transform);
            }
        }
    }
}