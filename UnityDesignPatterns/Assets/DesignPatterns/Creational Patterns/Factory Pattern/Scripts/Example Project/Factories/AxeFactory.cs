using UnityEngine;

namespace DesignPatterns.Factory
{
    public class AxeFactory : Factory
    {
        // used to create a Prefab
        [SerializeField] private Axe axePrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.AxeFactory;
        public override float ProductionTime { get; set; } = 0.75f;


        public override IWeapon GetWeapon(Vector3 position)
        {
            // create a Prefab instance and get the product component
            GameObject instance = Instantiate(axePrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Axe axe = instance.GetComponent<Axe>();

            // each product contains its own logic
            axe.Initialize();

            return axe;
        }
    }
}