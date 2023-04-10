using Scripts.Logic.Animations;
using Scripts.Logic.Enemy;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic.Hero
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterDamage))]
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private int _damageAmount;
        [SerializeField] private TriggerObserver _trigger;
        private CharacterMovement _movement;
        private CharacterDamage _selfDamage;
        private CharacterAnimator _animator;
        private AnimationEventHandler _animationEventHandler;

        private void Awake()
        {
            _movement = GetComponent<CharacterMovement>();
            _selfDamage = GetComponent<CharacterDamage>();
            _animator = this.GetComponentInChildrenForSure<CharacterAnimator>();
            _animationEventHandler = this.GetComponentInChildrenForSure<AnimationEventHandler>();
        }

        private void OnEnable()
        {
            _trigger.TriggerEnter += OnTargetEnteredAttackZone;
            _trigger.TriggerExit += OnTargetExitAttackZone;
            _animationEventHandler.Attacked += OnAttackHandled;
        }

        private void OnDisable()
        {
            _trigger.TriggerEnter -= OnTargetEnteredAttackZone;
            _animationEventHandler.Attacked -= OnAttackHandled;
            StopAllCoroutines();
        }

        private void OnTargetEnteredAttackZone(Collider target)
        {
            _movement.enabled = false;
            _animator.SetAttackValue(true);
        }

        private void OnTargetExitAttackZone(Collider target)
        {
            _animator.SetAttackValue(false);
            this.Invoke(StopAttack, 0.5f);
        }

        private void StopAttack() =>
            _movement.enabled = true;

        private void OnAttackHandled()
        {
            int damageBack = 0;
            foreach (GameObject target in _trigger.TriggeredObjects)
            {
                target.GetComponent<IDamageable>().ApplyDamage(_damageAmount);
                damageBack += target.GetComponent<EnemyAttack>().DamageAmount;
            }

            _selfDamage.ApplyDamage(damageBack);
        }
    }
}