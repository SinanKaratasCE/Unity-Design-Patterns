using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Decorator
{
    public class ItemUI : MonoBehaviour
    {
        public Item item;
        [SerializeField] private TMP_Text itemNameText;
        [SerializeField] private TMP_Text itemPriceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button equipItemButton;
        [SerializeField] private Button removeItemButton;

        [Header("Item Stats")] [SerializeField]
        private TMP_Text itemDamageText;

        [SerializeField] private TMP_Text itemDiggingSpeedText;
        [SerializeField] private TMP_Text itemEfficiencyText;
        [SerializeField] private TMP_Text itemMovementSpeedText;

        private ItemShop _itemShop;

        private void Awake()
        {
            _itemShop = SingletonContainer.Instance.ItemShop;
        }

        private void Start()
        {
            buyButton.onClick.AddListener(()=>_itemShop.BuyItem(item));
            equipItemButton.onClick.AddListener(()=>_itemShop.EquipItem(item));
            removeItemButton.onClick.AddListener(()=>_itemShop.RemoveItem(item));
        }


        public void FillItemUI()
        {
            itemNameText.text = item.itemName.ToString();
            itemPriceText.text = $"{item.price} ";
            itemDamageText.text = $"Damage: {item.damageValue}";
            itemDiggingSpeedText.text = $"Digging Speed: {item.diggingSpeedValue}";
            itemEfficiencyText.text = $"Efficiency Multiplier : {item.efficiencyValue}";
            itemMovementSpeedText.text = $"Movement Speed: {item.movementSpeedValue}";
        }

        public void ChangeButtonState()
        {
            buyButton.gameObject.SetActive(false);
            removeItemButton.gameObject.SetActive(true);
        }
    }
}