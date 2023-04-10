using System;
using UnityEngine;

namespace Scripts.Logic.Animations
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action Attacked;

        public void OnAttack() => Attacked?.Invoke();
    }
}
