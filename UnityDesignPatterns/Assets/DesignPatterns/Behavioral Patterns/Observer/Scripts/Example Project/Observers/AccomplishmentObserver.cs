using UnityEngine;
using Zenject;
using Utils;

namespace DesignPatterns.Observer
{
    public class AccomplishmentObserver : MonoBehaviour
    {
        private PlayerBase _player;
        private SuccessSubject _successSubject;
        private FailSubject _failSubject;

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
            _successSubject.SuccessEvent += LoadNextLevel;
            _failSubject.FailEvent += LevelRestart;
        }

        private void RemoveObserver()
        {
            _successSubject.SuccessEvent -= LoadNextLevel;
            _failSubject.FailEvent -= LevelRestart;
        }

        private void LevelRestart()
        {
            ResetCharacterPosition();
        }

        private void LoadNextLevel()
        {
            //We have only one level so we reset the player :/
            ResetCharacterPosition();
        }

        private void ResetCharacterPosition()
        {
            _player.gameObject.SetActive(false);
            _player.transform.position = Vector3.zero;
            Timer.Instance.TimerWait(1f, () => _player.gameObject.SetActive(true));
        }
    }
}