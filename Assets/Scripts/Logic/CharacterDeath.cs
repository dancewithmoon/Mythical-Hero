using System;
using System.Collections.Generic;
using DG.Tweening;
using Scripts.Logic.Animations;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterController), typeof(CharacterDamage))]
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _componentsToDisable;
        private Health _health;
        private CharacterAnimator _animator;

        public bool IsDead { get; private set; }

        public event Action Dead;

        private void Awake()
        {
            _health = this.GetComponentInChildrenForSure<Health>();
            _animator = this.GetComponentInChildrenForSure<CharacterAnimator>();
        }

        private void OnEnable() => 
            _health.HealthChanged += OnHealthChanged;

        private void OnDisable() => 
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
            _animator.SetDeathTrigger();
            DisableComponents();
            Destroy(gameObject, 2f);
            Dead?.Invoke();
        }

        private void DisableComponents()
        {
            enabled = false;
            _componentsToDisable.ForEach(component => component.enabled = false);
        }
    }
}