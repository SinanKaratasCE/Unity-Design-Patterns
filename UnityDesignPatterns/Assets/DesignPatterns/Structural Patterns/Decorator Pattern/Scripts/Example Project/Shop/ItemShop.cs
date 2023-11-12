using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class ItemShop : MonoBehaviour
    {
        private UIManager _uiManager;
        [SerializeField] private GameObject shopTextMesh;
        [SerializeField] private Player player;
        [SerializeField] private List<ItemUI> itemUIList = new List<ItemUI>();

        private Item _selectedItem;
        private PlayerItemManager _playerItemManager;


        private void Start()
        {
            _uiManager = SingletonContainer.Instance.UIManager;
            _playerItemManager = player.GetComponent<PlayerItemManager>();
            FillAllItemsUI();
        }

        private void FillAllItemsUI()
        {
            foreach (var itemUI in itemUIList)
            {
                itemUI.FillItemUI();
            }
        }

        private void OnMouseDown()
        {
            //Open Item Shop UI
            _uiManager.OpenItemShopUI();
        }

        private void OnMouseEnter()
        {
            shopTextMesh.SetActive(true);
            player.CanDig = false;
        }

        private void OnMouseExit()
        {
            shopTextMesh.SetActive(false);
            player.CanDig = true;
        }

        public void BuyItem(Item item)
        {
            _selectedItem = item;
            _uiManager.ShowDecisionPanel();
        }

        public void CheckMoneyEnough()
        {
            if (player.CheckPlayerMoney(_selectedItem))
            {
                player.BuyItem(_selectedItem);
                _uiManager.CloseDecisionPanel();
                ChangeItemUIButtonState();
            }
            else
            {
                _uiManager.ShowInsufficientMoneyText();
            }
        }

        private void ChangeItemUIButtonState()
        {
            foreach (var itemUI in itemUIList)
            {
                if (itemUI.item == _selectedItem)
                {
                    itemUI.ChangeButtonState();
                }
            }
        }

        public void EquipItem(Item item)
        {
            _playerItemManager.EquipItem(item);
        }
        
        public void RemoveItem(Item item)
        {
            _playerItemManager.RemoveItem(item);
        }
    }
}