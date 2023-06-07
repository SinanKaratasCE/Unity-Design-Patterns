using System;
using UnityEngine;

namespace DesignPatterns.Observer
{
    public class SuccessSubject : MonoBehaviour
    {
        public event Action SuccessEvent;
        public void LevelSucceeded()
        {
            SuccessEvent?.Invoke();
        }
    }
}