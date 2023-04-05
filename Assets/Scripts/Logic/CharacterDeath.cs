using Scripts.Logic.Animations;
using Scripts.Logic.Hero;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterDamage), typeof(CharacterController))]
    public class CharacterDeath : MonoBehaviour
    {
        [SerializeField] private HeroAttack _attack; //TODO: Need more abstraction
        
        [SerializeField] private Health _health;
        [SerializeField] private CharacterAnimator _animator;
        private CharacterMovement _movement;
        private CharacterDamage _damage;
        private CharacterController _characterController;

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _damage = GetComponent<CharacterDamage>();
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
            if (_attack != null)
            {
                _attack.enabled = false;
            }
            
            _movement.enabled = false;
            _damage.enabled = false;
            _characterController.enabled = false;
        }
    }
}