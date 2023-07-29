using UnityEngine;

namespace DesignPatterns.Factory
{
    public class HammerFactory : Factory
    {
        [SerializeField] private Hammer hammerPrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.HammerFactory;
        public override float ProductionTime { get; set; } = 1f;


        public override IWeapon GetWeapon(Vector3 position)
        {
            GameObject instance = Instantiate(hammerPrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Hammer hammer = instance.GetComponent<Hammer>();

            hammer.Initialize();

            return hammer;
        }
    }
}