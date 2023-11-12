using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DesignPatterns.Decorator
{
    public class PlayerFeatureChanger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> itemList = new List<GameObject>();
        [SerializeField] private Material diamondCoatingMaterial;
        private Material defaultMaterial;

        private TPSPlayerController _playerController;
        private Player _player;
        private Animator _animator;
        private UIManager _uiManager;
        
        

        private void Start()
        {
            _playerController = GetComponent<TPSPlayerController>();
            _player = GetComponent<Player>();
            _animator = GetComponent<Animator>();
            _uiManager = SingletonContainer.Instance.UIManager;
            defaultMaterial = itemList[(int)ItemName.DiamondCoating].GetComponent<MeshRenderer>().material;
        }


        public void AddOrRemoveSpeedyBootsVisual()
        {
            //Add Movement Speed
            itemList[(int)ItemName.SpeedyBoots].SetActive(!itemList[(int)ItemName.SpeedyBoots].activeSelf);
        }

        public void AddOrRemoveLightGloves()
        {
            //Change Animation Speed
            itemList[(int)ItemName.LightGloves].SetActive(!itemList[(int)ItemName.LightGloves].activeSelf);
        }


        public void AddOrRemoveEfficiencyStonesVisual()
        {
            //Add Productivity
            itemList[(int)ItemName.EfficiencyStones].SetActive(!itemList[(int)ItemName.EfficiencyStones].activeSelf);
        }

        public void AddOrRemoveDiamondCoatingVisual()
        {
            //Add Damage
             itemList[(int)ItemName.DiamondCoating].GetComponent<MeshRenderer>().material =
                 itemList[(int)ItemName.DiamondCoating].GetComponent<MeshRenderer>().material.mainTexture != diamondCoatingMaterial.mainTexture
                     ? diamondCoatingMaterial
                     : defaultMaterial;
            
        }

        public void SetCharacterStatus()
        {
            _playerController.MoveSpeed = _player.characterStatus.MovementSpeedEffect();
            _animator.SetFloat(Strings.DiggingSpeedAnimation, _player.characterStatus.DiggingSpeedEffect());
            _player.EfficiencyMultiplier = _player.characterStatus.EfficiencyEffect();
            _player.Damage = (int)_player.characterStatus.DamageEffect();
            _uiManager.playerStatsUI.UpdatePlayerStats(_player.characterStatus);
        }

        public void HidePlayerItemVisuals()
        {
            AddOrRemoveLightGloves();
            AddOrRemoveSpeedyBootsVisual();
            AddOrRemoveEfficiencyStonesVisual();
            AddOrRemoveDiamondCoatingVisual();
        }
    }
}