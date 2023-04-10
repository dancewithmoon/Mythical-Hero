using UnityEngine;

namespace Scripts.Logic.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damageAmount;

        public int DamageAmount => _damageAmount;
    }
}
