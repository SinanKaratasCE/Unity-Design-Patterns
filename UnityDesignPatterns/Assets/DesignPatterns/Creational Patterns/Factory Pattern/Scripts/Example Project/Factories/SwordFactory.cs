using UnityEngine;

namespace DesignPatterns.Factory
{
    public class SwordFactory : Factory
    {
        [SerializeField] private Sword swordPrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.SwordFactory;
        public override float ProductionTime { get; set; } =0.75f;


        public override IWeapon GetWeapon(Vector3 position)
        {
            GameObject instance = Instantiate(swordPrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Sword sword = instance.GetComponent<Sword>();

            sword.Initialize();

            return sword;
        }
    }
}