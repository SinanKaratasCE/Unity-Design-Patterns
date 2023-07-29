using System;
using UnityEngine;
using DG.Tweening;

namespace DesignPatterns.Factory
{
    public class TimeProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject progressBar;

        private FactoryManager _factoryManager;

        private void Start()
        {
            _factoryManager = SingletonContainer.Instance.FactoryManager;
        }


        public void AnimateBar(float timeToFill)
        {
            progressBar.transform.DOScaleX(1f, timeToFill).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                AnimationComplete();
            });
        }

        private void AnimationComplete()
        {
            ResetBar();
            _factoryManager.ProduceWeapon();
        }

        private void ResetBar()
        {
            progressBar.transform.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}