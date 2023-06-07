using UnityEngine;
using System;

namespace DesignPatterns.Observer
{
    public class FailSubject : MonoBehaviour
    {
        public event Action FailEvent;
        public void LevelFailed()
        {
            FailEvent?.Invoke();
        }
    }
}