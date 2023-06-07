using UnityEngine;
using Zenject;

namespace DesignPatterns.Observer
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private bool isRightGround;
        private FailSubject _failSubject;
        private SuccessSubject _successSubject;
        
        [Inject]
        private void OnInstaller(FailSubject failSubject, SuccessSubject successSubject)
        {
            _failSubject = failSubject;
            _successSubject = successSubject;
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<PlayerBase>())
            {
                if (isRightGround)
                {
                    _successSubject.LevelSucceeded();
                }
                else
                {
                    _failSubject.LevelFailed();
                }
            }
        }
    }
}