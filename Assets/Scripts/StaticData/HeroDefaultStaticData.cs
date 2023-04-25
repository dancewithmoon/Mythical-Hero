using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
    public class HeroDefaultStaticData : ScriptableObject
    {
        [Range(1, 100)] 
        [SerializeField] private int _health = 50;
        
        [Range(1, 50)] 
        [SerializeField] private int _damage = 5;

        public int Health => _health;
        public int Damage => _damage;
    }
}