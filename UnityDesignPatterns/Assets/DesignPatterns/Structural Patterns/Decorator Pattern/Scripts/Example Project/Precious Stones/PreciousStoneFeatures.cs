using UnityEngine;

namespace MyNameSpace
{
    [CreateAssetMenu(fileName = "New Precious Stone", menuName = "Precious Stone")]
    public class PreciousStoneFeatures : ScriptableObject
    {
        public int prize;
        public int durability;
        public string name;
        public ParticleSystem hitParticlePrefab;
    }

}