using System.Collections;
using System.Collections.Generic;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic.Animations;
using Scripts.Logic.Enemy;
using Scripts.Logic.Hero.Animations;
using Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Hero
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterDamage))]
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private TriggerObserver _trigger;
        private int _damageAmount;
        private CharacterMovement _movement;
        private CharacterDamage _selfDamage;
        private HeroAnimator _animator;
        private AnimationEventHandler _animationEventHandler;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _damageAmount = progressService.Progress.Damage;
            
            _movement = GetComponent<CharacterMovement>();
            _selfDamage = GetComponent<CharacterDamage>();
            _animator = this.GetComponentInChildrenForSure<HeroAnimator>();
            _animationEventHandler = this.GetComponentInChildrenForSure<AnimationEventHandler>();
        }

        private void OnEnable() => 
            _animationEventHandler.Attacked += OnAttackHandled;

        private void OnDisable()
        {
            _animationEventHandler.Attacked -= OnAttackHandled;
            StopAllCoroutines();
        }

        private void Start() => 
            StartCoroutine(WaitForEnemyAndAttack());

        private IEnumerator WaitForEnemyAndAttack()
        {
            while (this != null)
            {
                yield return new WaitUntil(IsAnyEnemyDetected);
                StartAttack();
                yield return new WaitForSeconds(_cooldown);
            }
        }

        private void StartAttack()
        {
            _movement.enabled = false;
            _animator.SetAttackTrigger();
        }

        private void OnAttackHandled()
        {
            List<GameObject> targets = new List<GameObject>(_trigger.TriggeredObjects);

            Attack(targets, out int damageBack);
            
            if (AreSomeTargetsStillAlive(targets))
            {
                _selfDamage.ApplyDamage(damageBack);
            }
            else
            {
                _movement.enabled = true;
            }
        }

        private void Attack(List<GameObject> targets, out int damageBack)
        {
            damageBack = 0;
            foreach (GameObject target in targets)
            {
                target.GetComponent<IDamageable>().ApplyDamage(_damageAmount);
                damageBack += target.GetComponent<EnemyAttack>().DamageAmount;
            }
        }

        private bool IsAnyEnemyDetected() => _trigger.TriggeredObjects.Count > 0;

        private static bool AreSomeTargetsStillAlive(List<GameObject> targets) => 
            targets.Exists(target => target.GetComponent<CharacterDeath>().IsDead == false);
    }
}