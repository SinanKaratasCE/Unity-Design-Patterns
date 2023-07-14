using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    
    [CreateAssetMenu(fileName = "CharacterStatsSO", menuName = "ScriptableObjects/CharacterStatsSO", order = 1)]
    public class CharacterStatsSO : ScriptableObject
    {

       public float Health = 100f;
       public float Damage = 10f;
 
    }

}