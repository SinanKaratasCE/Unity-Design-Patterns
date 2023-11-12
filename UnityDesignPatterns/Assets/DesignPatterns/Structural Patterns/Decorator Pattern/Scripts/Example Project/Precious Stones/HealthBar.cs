using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Utils;

namespace DesignPatterns.Decorator
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarSprite;
        [SerializeField] private float animationDuration = 0.1f;
        private float _perTickHealth;
        private float _target = 1f;
        private float _maxHealth;
        private GameObject _targetObject;


        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            healthBarSprite.fillAmount = 1f;
            CalculatePerTickHealthAmount();
        }

        private void CalculatePerTickHealthAmount()
        {
            _perTickHealth = 1f / _maxHealth;
        }

        public void ShowHealthBar()
        {
            gameObject.SetActive(true);
        }

        public void HideHealthBar()
        {
            Timer.Instance.TimerWait(0.75f, () => gameObject.SetActive(false));
        }

        public void DecreaseHealth(float damage, GameObject targetObject)
        {
            _targetObject = targetObject;
            _target = healthBarSprite.fillAmount - (damage * _perTickHealth);

            if (_target < 0)
                _target = 0;
            HealthReduceAnimation();
        }

        public void IncreaseHealth(float heal)
        {
            _target = healthBarSprite.fillAmount + (heal * _perTickHealth);
            if (_target > _maxHealth)
                return;
            HealthIncreaseAnimation();
        }

        private void HealthReduceAnimation()
        {
            DOTween.To(() => healthBarSprite.fillAmount, x => healthBarSprite.fillAmount = x, _target,
                animationDuration).OnComplete(DeActivateTargetObject);
        }

        private void HealthIncreaseAnimation()
        {
            DOTween.To(() => healthBarSprite.fillAmount, x => healthBarSprite.fillAmount = x, _target,
                animationDuration);
        }

        private void DeActivateTargetObject()
        {
            if (_target > 0)
                return;
            _targetObject.SetActive(false);
        }
    }
}