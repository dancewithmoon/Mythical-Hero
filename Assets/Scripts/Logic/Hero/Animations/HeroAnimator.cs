using System;
using Scripts.Logic.Animations;
using UnityEngine;

namespace Scripts.Logic.Hero.Animations
{
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : CharacterAnimator, IAnimatorStateReader
    {
        private static readonly int AttackParameterHash = Animator.StringToHash("Attack");
        private static readonly int StunParameterHash = Animator.StringToHash("ToStun");
        private static readonly int DeathParameterHash = Animator.StringToHash("Death");

        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _skillHash = Animator.StringToHash("Skill");
        private readonly int _stunHash = Animator.StringToHash("Stun");
        private readonly int _debuffHash = Animator.StringToHash("Debuff");

        private Animator _animator;
        private AnimatorState _state;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State
        {
            get => _state;
            private set
            {
                if (Equals(_state, value))
                    return;

                StateExited?.Invoke(_state);
                _state = value;
                StateEntered?.Invoke(_state);
            }
        }

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public override void SetAttackTrigger() => 
            _animator.SetTrigger(AttackParameterHash);

        public override void SetStunTrigger()
        {
            if(_animator.GetBool(DeathParameterHash))
                return;
            
            _animator.SetTrigger(StunParameterHash);
        }

        public override void SetDeathTrigger() => 
            _animator.SetTrigger(DeathParameterHash);

        public void EnteredState(int stateHash) => 
            State = GetStateByHash(stateHash);

        public void ExitedState(int stateHash) { }

        private AnimatorState GetStateByHash(int stateHash)
        {
            if (stateHash == _runStateHash)
                return AnimatorState.Run;
            
            if (stateHash == _attackStateHash)
                return AnimatorState.Attack;
            
            if (stateHash == _skillHash)
                return AnimatorState.Skill;

            if (stateHash == _stunHash)
                return AnimatorState.Stun;

            if (stateHash == _debuffHash)
                return AnimatorState.Debuff;
            
            return AnimatorState.Unknown;
        }
    }
}