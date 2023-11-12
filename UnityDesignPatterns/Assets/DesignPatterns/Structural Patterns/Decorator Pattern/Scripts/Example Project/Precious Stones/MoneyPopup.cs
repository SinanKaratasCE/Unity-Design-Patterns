using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = UnityEngine.Random;

namespace DesignPatterns.Decorator
{
    public class MoneyPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupText;
        [SerializeField] private GameObject popupOriginTransform;
        private float _randomValueForPopup;
        private const float AnimationTime = 0.3f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }


        public void ShowPopUpText(int damage)
        {
            popupText.SetActive(true);
            popupText.transform.position = popupOriginTransform.transform.position;
            popupText.GetComponent<TextMeshPro>().text = "" + damage;
            _randomValueForPopup = Random.Range(2.5f, 2.6f);
            popupText.transform
                .DOMove(
                    new Vector3(popupOriginTransform.transform.position.x, popupOriginTransform.transform.position.y + _randomValueForPopup,
                        popupOriginTransform.transform.position.z), AnimationTime).OnComplete(() => HidePopUpText(popupText));
            popupText.transform.DOScale(0.2f, AnimationTime);
        }

        private void HidePopUpText(GameObject popupText)
        {
            popupText.transform.DOMove(popupOriginTransform.transform.position, AnimationTime)
                .OnComplete(() => PopupInactivate(popupText));
            popupText.transform.DOScale(0.04f, AnimationTime);
        }

        private void PopupInactivate(GameObject popupText)
        {
            popupText.SetActive(false);
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}