using System;
using UnityEngine;

namespace Scripts.Hero
{
    public class HeroAnimationEventHandler : MonoBehaviour
    {
        public event Action Attacked;

        public void OnAttack() => Attacked?.Invoke();
    }
}
