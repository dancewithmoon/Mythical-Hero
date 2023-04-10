using System.Collections.Generic;
using Scripts.Logic.Animations;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _componentsToDisable;
        private Health _health;
        private CharacterAnimator _animator;
        private CharacterController _characterController;

        private void Awake()
        {
            _health = this.GetComponentInChildrenForSure<Health>();
            _animator = this.GetComponentInChildrenForSure<CharacterAnimator>();
            _characterController = GetComponent<CharacterController>();
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
            _animator.SetDeathTrigger();

            this.Invoke(DisableComponents, 0.6f);
            Destroy(gameObject, 2f);
        }

        private void DisableComponents()
        {
            _characterController.enabled = false;
            _componentsToDisable.ForEach(component => component.enabled = false);
        }
    }
}