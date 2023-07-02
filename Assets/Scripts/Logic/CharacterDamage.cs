using System.Collections;
using Scripts.Logic.Animations;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterController), typeof(CharacterMovement))]
    public class CharacterDamage : MonoBehaviour, IDamageable
    {
        [Header("Parameters")]
        [SerializeField] private float _pushbackDuration = 0.25f;
        [SerializeField] private Direction _pushbackDirection;
        [SerializeField] private float _stunDuration = 0.5f;
        private float _pushbackForce;
        private CharacterAnimator _animator;
        private Health _health;
        private CharacterMovement _movement;
        private CharacterController _characterController;

        public bool IsGettingDamage { get; private set; }
        
        private void Awake()
        {
            _animator = this.GetComponentInChildrenForSure<CharacterAnimator>();
            _health = this.GetComponentInChildrenForSure<Health>();
            _movement = GetComponent<CharacterMovement>();
            _characterController = GetComponent<CharacterController>();
        }
        
        private void OnDisable() => StopAllCoroutines();

        public void ApplyDamage(int amount)
        {
            if(enabled == false)
                return;
            
            if(IsGettingDamage)
                return;
            
            _pushbackForce = amount;
            
            _health.ApplyDamage(amount);

            if(_health.Current <= 0)
                return;
            
            StartCoroutine(ApplyDamageCoroutine());
        }

        private IEnumerator ApplyDamageCoroutine()
        {
            IsGettingDamage = true;
            _movement.enabled = false;

            if (_animator)
            {
                _animator.SetStunTrigger();
            }

            yield return StartCoroutine(ExecutePushback());
            yield return StartCoroutine(ExecuteStun());

            IsGettingDamage = false;
            _movement.enabled = true;
        }

        private IEnumerator ExecutePushback()
        {
            Vector3 move = GetMovementVector();

            for (float time = 0; time < _pushbackDuration; time += Time.deltaTime)
            {
                _characterController.Move(move * Time.deltaTime);
                yield return null;
            }
        }

        private IEnumerator ExecuteStun()
        {
            yield return new WaitForSeconds(_stunDuration);
        }

        private Vector3 GetMovementVector()
        {
            Vector3 currentPosition = transform.position;
            Vector3 nextPosition = currentPosition + Vector3.right * (_pushbackForce * (int)_pushbackDirection);
            return nextPosition - currentPosition;
        }
    }
}