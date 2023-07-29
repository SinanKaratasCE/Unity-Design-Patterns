using UnityEngine;

namespace DesignPatterns.Factory
{
    public class WandFactory : Factory
    {
        [SerializeField] private Wand wandPrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.WandFactory;
        public override float ProductionTime { get; set; } =1.5f;


        public override IWeapon GetWeapon(Vector3 position)
        {
            GameObject instance =
                Instantiate(wandPrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Wand wand = instance.GetComponent<Wand>();

            wand.Initialize();

            return wand;
        }
    }
}