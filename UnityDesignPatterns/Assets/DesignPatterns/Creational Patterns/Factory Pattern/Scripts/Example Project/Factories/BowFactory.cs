using UnityEngine;

namespace DesignPatterns.Factory
{
    public class BowFactory : Factory
    {
        [SerializeField] private Bow bowPrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.BowFactory;
        public override float ProductionTime { get; set; } = 1.25f;

        public override IWeapon GetWeapon(Vector3 position)
        {
            GameObject instance = Instantiate(bowPrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Bow bow = instance.GetComponent<Bow>();

            bow.Initialize();

            return bow;
        }
    }
}