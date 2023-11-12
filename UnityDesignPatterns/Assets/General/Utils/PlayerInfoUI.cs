using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{

    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField] private Button infoButton;
        [SerializeField] private GameObject infoPanel;

        private void Start()
        {
            infoButton.onClick.AddListener(ManageInfoPanel);
        }
        
        private void ManageInfoPanel()
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }

}