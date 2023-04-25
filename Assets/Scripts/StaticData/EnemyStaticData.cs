using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyTypeId _enemyType;

        [Range(1, 100)] 
        [SerializeField] private int _health = 50;

        [Range(1, 50)] 
        [SerializeField] private int _damage = 5;
        
        public EnemyTypeId EnemyType => _enemyType;
        public int Health => _health;
        public int Damage => _damage;
    }
    
    public enum EnemyTypeId
    {
        Lich = 0,
    }
}