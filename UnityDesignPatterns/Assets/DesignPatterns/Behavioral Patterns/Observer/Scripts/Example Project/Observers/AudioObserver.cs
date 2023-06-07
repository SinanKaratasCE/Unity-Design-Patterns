using UnityEngine;
using Zenject;

namespace DesignPatterns.Observer
{
    public class AudioObserver : MonoBehaviour
    {
        private SuccessSubject _successSubject;
        private FailSubject _failSubject;
        
        [SerializeField] private AudioSource winAudioSource;
        [SerializeField] private AudioSource loseAudioSource;
        
        [Inject]
        private void OnInstaller(SuccessSubject successSubject, FailSubject failSubject)
        {
            _successSubject = successSubject;
            _failSubject = failSubject;
        }
        
        #region Unity Methods

        private void Awake()
        {
            RegisterObserver();
        }

        private void OnDestroy()
        {
            RemoveObserver();
        }

        #endregion

        private void RegisterObserver()
        {
            _successSubject.SuccessEvent += PlayWinSound;
            _failSubject.FailEvent += PlayLoseSound;
        }

        private void RemoveObserver()
        {
            _successSubject.SuccessEvent -= PlayWinSound;
            _failSubject.FailEvent -= PlayLoseSound;
        }

        private void PlayWinSound()
        {
            winAudioSource.Play();
        }

        private void PlayLoseSound()
        {
            loseAudioSource.Play();
        }
    }
}