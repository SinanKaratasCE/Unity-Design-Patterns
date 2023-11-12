using System;
using UnityEngine;

namespace DesignPatterns.Decorator
{

    public class Treasure : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = SingletonContainer.Instance.GameManager;
        
        }

        private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               _gameManager.GameOver();
           }
       }
 
    }

}