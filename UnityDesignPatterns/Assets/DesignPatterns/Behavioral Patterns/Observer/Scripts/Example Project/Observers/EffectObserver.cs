using UnityEngine;
using System;
using UnityEngine.Serialization;
using Zenject;

namespace DesignPatterns.Observer
{
    public class EffectObserver : MonoBehaviour
    {
        private SuccessSubject _successSubject;
        private FailSubject _failSubject;
        private PlayerBase _player;

        [SerializeField] private ParticleSystem winParticle;
        [SerializeField] private ParticleSystem loseParticle;

        [Inject]
        private void OnInstaller(SuccessSubject successSubject, FailSubject failSubject, PlayerBase player)
        {
            _successSubject = successSubject;
            _failSubject = failSubject;
            _player = player;
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
            _successSubject.SuccessEvent += PlayWinParticle;
            _failSubject.FailEvent += PlayLoseParticle;
        }

        private void RemoveObserver()
        {
            _successSubject.SuccessEvent -= PlayWinParticle;
            _failSubject.FailEvent -= PlayLoseParticle;
        }

        private void PlayWinParticle()
        {
            winParticle.transform.position = _player.transform.position;
            winParticle.Play();
        }

        private void PlayLoseParticle()
        {
            loseParticle.transform.position = _player.transform.position;
            loseParticle.Play();
        }
    }
}