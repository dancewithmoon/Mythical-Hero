using UnityEngine;

namespace Scripts.Logic.Animations
{
    public abstract class CharacterAnimator : MonoBehaviour
    {
        public abstract void SetAttackTrigger();
        public abstract void SetStunTrigger();
        public abstract void SetDeathTrigger();
    }
}