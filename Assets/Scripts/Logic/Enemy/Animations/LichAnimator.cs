using Scripts.Logic.Animations;
using UnityEngine;

namespace Scripts.Logic.Enemy.Animations
{
    [RequireComponent(typeof(Animator))]
    public class LichAnimator : CharacterAnimator
    {
        private static readonly int AttackParameterHash = Animator.StringToHash("Attack");
        private static readonly int StunParameterHash = Animator.StringToHash("ToStun");
        
        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public override void SetAttackValue(bool value) => 
            _animator.SetBool(AttackParameterHash, value);

        public override void SetStunTrigger() => 
            _animator.SetTrigger(StunParameterHash);
    }
}