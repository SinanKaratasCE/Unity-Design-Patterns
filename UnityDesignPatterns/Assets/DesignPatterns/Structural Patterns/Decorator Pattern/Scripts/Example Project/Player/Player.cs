using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace DesignPatterns.Decorator
{
    public class Player : MonoBehaviour
    {
        [Header("References")] private PlayerInput _playerInput;
        private Animator _animator;
        private UIManager _uiManager;
        private TPSPlayerController _tpsPlayerController;
        private PlayerFeatureChanger _playerFeatureChanger;
        private IsPointerOverUI _isPointerOverUI;
        private VFXSpawner _vfxSpawner;


        [Header("Raycast")] private Ray _ray;
        [SerializeField] private float maxDistance = 1f;
        [SerializeField] private LayerMask stonesLayerMask;
        [SerializeField] private Transform raycastOrigin;


        [Header("Character Items and Status")] private PlayerItemManager _playerItemManager;
        public StatusEffect characterStatus { get; set; }
        public int Damage { get; set; } = 10;
        public bool CanDig { get; set; } = true;

        [Header("Player Income")] [SerializeField]
        private int money;

        private int _gainedMoney;
        private ParticleSystem _particle;

        public float EfficiencyMultiplier { get; set; }


        private void Awake()
        {
            GetAllReferences();
        }


        private void GetAllReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
            _animator = GetComponent<Animator>();
            _tpsPlayerController = GetComponent<TPSPlayerController>();
            _playerItemManager = GetComponent<PlayerItemManager>();
            _playerFeatureChanger = GetComponent<PlayerFeatureChanger>();
            _isPointerOverUI = GetComponent<IsPointerOverUI>();
            _vfxSpawner = SingletonContainer.Instance.VFXSpawner;
        }

        private void GetPlayerBaseStatus()
        {
            characterStatus = new PlayerBaseStatus();
            _playerFeatureChanger.SetCharacterStatus();
        }


        private void Start()
        {
            GetPlayerBaseStatus();
            _uiManager = SingletonContainer.Instance.UIManager;
            _uiManager.UpdateGoldText(money);
           
        }

        private void Update()
        {
            if (_isPointerOverUI.IsPointerOverUIElement()) return;

            if (CanDig && _playerInput.IsDiggingInput)
            {
                Dig();
            }

            Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward, Color.red, maxDistance);
        }

        private void AddPreciousStone(PreciousStone preciousStone)
        {
            _gainedMoney = (int)(preciousStone.GetPrize() * EfficiencyMultiplier);
            money += _gainedMoney;
            preciousStone.TakeDamage(Damage, _gainedMoney);
            _uiManager.UpdateGoldText(money);
        }


        private void Dig()
        {
            CanDig = false;
            _tpsPlayerController.CanMove = false;
            _animator.SetTrigger(Strings.DigAnimation);
        }

        private void AnimationEvent_OnHit()
        {
            _ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

            if (!Physics.Raycast(_ray, out var hit, maxDistance, stonesLayerMask)) return;
            if (hit.collider.TryGetComponent(out PreciousStone preciousStone))
            {
                AddPreciousStone(preciousStone);
                CreateAndPlayParticle(hit);
                
            }
            
        }
        
        
        private void CreateAndPlayParticle( RaycastHit hit)
        {
            _particle = _vfxSpawner.Spawn(hit.point).GetComponent<ParticleSystem>();
            _particle.Play();
        }

        public void AnimationEvent_OnAnimationEnd()
        {
            _tpsPlayerController.CanMove = true;
            CanDig = true;
        }

        public bool CheckPlayerMoney(Item item)
        {
            return money >= item.price;
        }

        public void BuyItem(Item item)
        {
            //If player has enough money
            _playerItemManager.EquipItem(item);
            money -= item.price;
            _uiManager.UpdateGoldText(money);
        }

        public void AddOrRemoveItemFeaturesByType(ItemName itemName)
        {
            switch (itemName)
            {
                case ItemName.SpeedyBoots:
                    _playerFeatureChanger.AddOrRemoveSpeedyBootsVisual();
                    break;
                case ItemName.LightGloves:
                    _playerFeatureChanger.AddOrRemoveLightGloves();
                    break;
                case ItemName.EfficiencyStones:
                    _playerFeatureChanger.AddOrRemoveEfficiencyStonesVisual();
                    break;
                case ItemName.DiamondCoating:
                    _playerFeatureChanger.AddOrRemoveDiamondCoatingVisual();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemName), itemName, null);
            }

            _playerFeatureChanger.SetCharacterStatus();
        }

        public void ResetPlayerProperties()
        {
            _playerFeatureChanger.HidePlayerItemVisuals();
            _playerItemManager.ResetPlayerItems();
            _tpsPlayerController.ResetPlayerPosition();
            GetPlayerBaseStatus();
        }

        


        
    }
}