using UnityEngine;
using Utils;
using Zenject;

namespace DesignPatterns.Observer
{
    public class PlayerViewObserver : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private SuccessSubject _successSubject;
        private FailSubject _failSubject;

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
            _successSubject.SuccessEvent += ShowWinPanel;
            _failSubject.FailEvent += ShowLosePanel;
        }

        private void RemoveObserver()
        {
            _successSubject.SuccessEvent -= ShowWinPanel;
            _failSubject.FailEvent -= ShowLosePanel;
        }

        private void ShowWinPanel()
        {
            winPanel.SetActive(true);
            Timer.Instance.TimerWait(0.5f, () => winPanel.SetActive(false));
        }

        private void ShowLosePanel()
        {
            losePanel.SetActive(true);
            Timer.Instance.TimerWait(0.5f, () => losePanel.SetActive(false));
        }
    }
}