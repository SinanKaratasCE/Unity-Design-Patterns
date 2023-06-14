using UnityEngine;
using Zenject;
using DG.Tweening;
using Utils;

namespace DesignPatterns.Observer
{
    public class AnimObserver : MonoBehaviour
    {
        private SuccessSubject _successSubject;
        private FailSubject _failSubject;
        private PlayerBase _player;
        private float characterCurrentScale = 1f;

        [Inject]
        private void OnInstaller(SuccessSubject successSubject, FailSubject failSubject, PlayerBase player)
        {
            _successSubject = successSubject;
            _failSubject = failSubject;
            _player = player;
        }

        #region Unity Methods

        void Awake()
        {
            RegisterObserver();
        }

        void OnDestroy()
        {
            RemoveObserver();
        }

        #endregion

        private void RegisterObserver()
        {
            _successSubject.SuccessEvent += PlayWinAnimation;
            _failSubject.FailEvent += PlayLoseAnimation;
        }

        private void RemoveObserver()
        {
            _successSubject.SuccessEvent -= PlayWinAnimation;
            _failSubject.FailEvent -= PlayLoseAnimation;
        }

        private void PlayWinAnimation()
        {
            Timer.Instance.TimerWait(1.1f, () => _player.transform.DOScale(characterCurrentScale += 0.02f, 0.1f));
        }

        private void PlayLoseAnimation()
        {
            Timer.Instance.TimerWait(1.1f, () => _player.transform.DOScale(characterCurrentScale -= 0.02f, 0.1f));
        }
    }
}