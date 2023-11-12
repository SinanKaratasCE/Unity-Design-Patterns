using System;
using DesignPatterns.SingletonObjectPool;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace DesignPatterns.Strategy
{
    public class WandChanger : MonoBehaviour
    {
        public GameObject[] wands;
        public int currentWand = 0;
        public bool canChange;

        private Animator _animator;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        void Start()
        {
            ActivateCurrentWand();
        }

        void Update()
        {
            if ((int)_playerInput.currentWand != currentWand)
            {
                SetWand(((int)_playerInput.currentWand));
            }
        }

        void SetWand(int index)
        {
            if (index == currentWand || !canChange)
                return;
            currentWand = index;
            canChange = false;
            PlayWandChangeAnimation();
        }

        private void PlayWandChangeAnimation()
        {
            _animator.SetTrigger(Strings.WandChangeAnimation);
        }

        private void DeactivateAllWands()
        {
            foreach (var wand in wands)
            {
                wand.SetActive(false);
            }
        }

        void ActivateCurrentWand()
        {
            if (currentWand < 0 || currentWand >= wands.Length)
            {
                return;
            }

            for (int i = 0; i < wands.Length; i++)
            {
                wands[i].SetActive(i == currentWand);
            }

            canChange = true;
        }

        private void CastSpellFromActiveWand()
        {
            wands[currentWand].GetComponent<SpellCaster>().CastSpell();
        }
    }
}