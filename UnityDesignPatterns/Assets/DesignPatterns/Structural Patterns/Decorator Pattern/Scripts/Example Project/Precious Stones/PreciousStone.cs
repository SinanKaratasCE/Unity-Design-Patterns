using MyNameSpace;
using TMPro;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class PreciousStone : MonoBehaviour
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private MoneyPopup moneyPopup;


        public PreciousStoneFeatures preciousStoneFeatures;


        private void Start()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.SetMaxHealth(GetDurability());
            nameText.text = GetName();
        }

        public virtual int GetPrize()
        {
            return 0;
        }

        public virtual int GetDurability()
        {
            return 0;
        }

        public virtual string GetName()
        {
            return "";
        }

        public void TakeDamage(int damage, int gainedMoney)
        {
            //Show HealthBar when taking damage
            healthBar.ShowHealthBar();
            moneyPopup.ShowPopUpText(gainedMoney);
            healthBar.DecreaseHealth(damage, gameObject);
        }

        public void ResetStone()
        {
            gameObject.SetActive(true);
            healthBar.SetMaxHealth(GetDurability());
        }


        private void OnMouseEnter()
        {
            healthBar.ShowHealthBar();
        }

        private void OnMouseExit()
        {
            healthBar.HideHealthBar();
        }
    }
}