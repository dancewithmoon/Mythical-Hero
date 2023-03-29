using System;
using UnityEngine;

namespace Scripts.Logic.Hero.Animations
{
    public class HeroAnimationEventHandler : MonoBehaviour
    {
        public event Action Attacked;

        public void OnAttack() => Attacked?.Invoke();
    }
}
