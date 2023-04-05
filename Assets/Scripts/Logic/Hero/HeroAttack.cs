using Scripts.Logic.Hero.Animations;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Logic.Hero
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterDamage))]
    [RequireComponent(typeof(HeroAnimator), typeof(HeroAnimationEventHandler))]
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private int _damageAmount;
        [SerializeField] private TriggerObserver _trigger;
        private CharacterMovement _heroMovement;
        private CharacterDamage _heroDamage;
        private HeroAnimator _heroAnimator;
        private HeroAnimationEventHandler _animationEventHandler;

        private void Awake()
        {
            _heroMovement = GetComponent<CharacterMovement>();
            _heroDamage = GetComponent<CharacterDamage>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _animationEventHandler = GetComponent<HeroAnimationEventHandler>();
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
        }

        private void OnTargetEnteredAttackZone(Collider target)
        {
            _heroMovement.enabled = false;
            _heroAnimator.SetAttackValue(true);
        }

        private void OnTargetExitAttackZone(Collider target)
        {
            _heroAnimator.SetAttackValue(false);
            this.Invoke(StopAttack, 0.5f);
        }

        private void StopAttack() => 
            _heroMovement.enabled = true;

        private void OnAttackHandled()
        {
            foreach (GameObject target in _trigger.TriggeredObjects)
            {
                target.GetComponent<IDamageable>().ApplyDamage(_damageAmount);
                _heroDamage.ApplyDamage(_damageAmount);
            }
        }
    }
}