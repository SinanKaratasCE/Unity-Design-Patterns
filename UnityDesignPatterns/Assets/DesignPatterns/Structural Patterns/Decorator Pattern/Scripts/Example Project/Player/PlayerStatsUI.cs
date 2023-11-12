using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Decorator
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text itemDamageText;
        [SerializeField] private TMP_Text itemDiggingSpeedText;
        [SerializeField] private TMP_Text itemEfficiencyText;
        [SerializeField] private TMP_Text itemMovementSpeedText;

        [SerializeField] private Button statsButton;
        [SerializeField] private GameObject statsPanel;

        private void Start()
        {
            statsButton.onClick.AddListener(ManageInfoPanel);
        }

        private void ManageInfoPanel()
        {
            statsPanel.SetActive(!statsPanel.activeSelf);
        }

        public void UpdatePlayerStats(StatusEffect playerStatus)
        {
            itemDamageText.text = $"Damage: {playerStatus.DamageEffect()}";
            itemDiggingSpeedText.text = $"Digging Speed: {playerStatus.DiggingSpeedEffect()}";
            itemEfficiencyText.text = $"Efficiency Multiplier : {playerStatus.EfficiencyEffect()}";
            itemMovementSpeedText.text = $"Movement Speed: {playerStatus.MovementSpeedEffect()}";
        }
        
    }
}