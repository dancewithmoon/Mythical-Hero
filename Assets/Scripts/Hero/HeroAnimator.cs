using System;
using Scripts.Logic.Animations;
using UnityEngine;

namespace Scripts.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimatorStateReader
    {
        private readonly int _runStateHash = Animator.StringToHash("Run");
        private readonly int _attack1StateHash = Animator.StringToHash("Attack1");
        private readonly int _attack2StateHash = Animator.StringToHash("Attack2");
        private readonly int _skillHash = Animator.StringToHash("Skill");
        private readonly int _stunHash = Animator.StringToHash("Stun");
        private readonly int _debuffHash = Animator.StringToHash("Debuff");

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

        public void EnteredState(int stateHash) => 
            State = GetStateByHash(stateHash);

        public void ExitedState(int stateHash) { }

        private AnimatorState GetStateByHash(int stateHash)
        {
            if (stateHash == _runStateHash)
                return AnimatorState.Run;
            
            if (stateHash == _attack1StateHash || stateHash == _attack2StateHash)
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