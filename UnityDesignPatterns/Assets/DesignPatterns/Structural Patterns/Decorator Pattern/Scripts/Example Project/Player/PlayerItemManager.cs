using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class PlayerItemManager : MonoBehaviour
    {
        public List<Item> heldItems = new List<Item>();

        private delegate void OnItemEquipped();

        private OnItemEquipped _onItemEquippedCallback;
        private Player _player;

        void Awake()
        {
            _player = GetComponent<Player>();
            _onItemEquippedCallback += UpdateCharacterState;
        }

        public void UpdateCharacterState()
        {
            _player.characterStatus = new PlayerBaseStatus();

            foreach (var name in heldItems)
            {
                if (heldItems.Count > 0)
                    _player.characterStatus =
                        StatusEffectDecoratorFactory.CreateStatusEffectDecorator(name, _player.characterStatus);
            }
        }

        public void EquipItem(Item item)
        {
            // ... equip logic
            heldItems.Add(item);

            // notify all observers about the event
            _onItemEquippedCallback?.Invoke();
            _player.AddOrRemoveItemFeaturesByType(item.itemName);
        }

        public void RemoveItem(Item item)
        {
            // ... unequipped logic
            heldItems.Remove(item);

            // notify all observers about the event
            _onItemEquippedCallback?.Invoke();
            _player.AddOrRemoveItemFeaturesByType(item.itemName);
        }
        
        public void ResetPlayerItems()
        {
            heldItems.Clear();
        }
    }
}