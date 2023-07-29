using UnityEngine;

namespace DesignPatterns.Factory
{
    public class DaggerFactory : Factory
    {
        [SerializeField] private Dagger daggerPrefab;
        public override FactoryType FactoryType { get; set; } = FactoryType.DaggerFactory;
        public override float ProductionTime { get; set; } = 0.5f;


        public override IWeapon GetWeapon(Vector3 position)
        {
            GameObject instance = Instantiate(daggerPrefab.gameObject, position, Quaternion.Euler(90f, 0f, 0f));
            Dagger dagger = instance.GetComponent<Dagger>();

            dagger.Initialize();

            return dagger;
        }
    }
}