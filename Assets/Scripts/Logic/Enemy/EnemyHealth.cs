using System;
using UnityEngine;

namespace Scripts.Logic.Enemy
{
    public class EnemyHealth : Health
    {
        [SerializeField] private int _current;
        [SerializeField] private int _max;

        public override int Current
        {
            get => _current;
            protected set => _current = Mathf.Clamp(value, 0, Max);
        }

        public override int Max
        {
            get => _max;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Max health haven't be less than 0");
                
                _max = value;
            }
        }

        public override event Action HealthChanged;

        public override void ApplyDamage(int amount)
        {
            if (amount == 0)
                return;

            _current = Mathf.Clamp(_current - amount, 0, Max);
            HealthChanged?.Invoke();
        }
    }
}