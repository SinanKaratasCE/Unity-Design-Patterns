using System;
using DesignPatterns.SingletonObjectPool;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Strategy
{
    public class WandChanger : MonoBehaviour
    {
        public GameObject[] wands;
        public int currentWand = 0;
        private Animator _animator;
        public bool canChange;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            ActivateCurrentWand();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetWand(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetWand(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetWand(2);
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
            _animator.SetTrigger("WandChange");
        }

        private void DeactivateAllWands()
        {
            foreach (var Wand in wands)
            {
                Wand.SetActive(false);
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