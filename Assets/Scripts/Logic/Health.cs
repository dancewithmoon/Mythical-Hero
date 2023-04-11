using System;
using UnityEngine;

namespace Scripts.Logic
{
    public abstract class Health : MonoBehaviour
    {
        public abstract int Current { get; protected set; }
        public abstract int Max { get; protected set; }

        public abstract event Action HealthChanged;

        public abstract void ApplyDamage(int amount);
    }
}
