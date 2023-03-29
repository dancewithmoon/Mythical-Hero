using System.Collections;
using Scripts.Logic.Animations;
using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterController), typeof(CharacterMovement))]
    public class CharacterDamage : MonoBehaviour, IDamageable
    {
        [Header("Parameters")]
        [SerializeField] private float _pushbackForce = 3;
        [SerializeField] private float _pushbackDuration = 0.25f;
        [SerializeField] private Direction _pushbackDirection;
        [SerializeField] private float _stunDuration = 0.5f;
        
        [Header("Components")]
        [SerializeField] private CharacterAnimator _animator;
        
        private CharacterMovement _movement;
        private CharacterController _characterController;

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _characterController = GetComponent<CharacterController>();
        }

        public void ApplyDamage()
        {
            StopAllCoroutines();
            StartCoroutine(ApplyDamageCoroutine());
        }

        private IEnumerator ApplyDamageCoroutine()
        {
            _movement.enabled = false;
            
            if (_animator)
            {
                _animator.SetStunTrigger();
            }

            yield return StartCoroutine(ExecutePushback());
            yield return StartCoroutine(ExecuteStun());
            
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