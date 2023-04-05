using UnityEngine;

namespace Scripts.Logic.Animations
{
    public abstract class CharacterAnimator : MonoBehaviour
    {
        public abstract void SetAttackValue(bool value);
        public abstract void SetStunTrigger();
        public abstract void SetDeathTrigger();
    }
}