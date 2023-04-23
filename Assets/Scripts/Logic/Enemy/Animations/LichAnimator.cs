using Scripts.Logic.Animations;
using UnityEngine;

namespace Scripts.Logic.Enemy.Animations
{
    [RequireComponent(typeof(Animator))]
    public class LichAnimator : CharacterAnimator
    {
        private static readonly int AttackParameterHash = Animator.StringToHash("Attack");
        private static readonly int StunParameterHash = Animator.StringToHash("ToStun");
        private static readonly int DeathParameterHash = Animator.StringToHash("Death");
        
        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public override void SetAttackTrigger() => 
            _animator.SetTrigger(AttackParameterHash);

        public override void SetStunTrigger() => 
            _animator.SetTrigger(StunParameterHash);

        public override void SetDeathTrigger() => 
            _animator.SetTrigger(DeathParameterHash);
    }
}