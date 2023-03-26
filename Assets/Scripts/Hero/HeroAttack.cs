using Scripts.Logic;
using UnityEngine;

namespace Scripts.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _trigger;
        private HeroAnimator _heroAnimator;
        
        private void Awake() => 
            _heroAnimator = GetComponent<HeroAnimator>();

        private void OnEnable() => 
            _trigger.TriggerEnter += OnTargetEnteredAttackZone;

        private void OnDisable() => 
            _trigger.TriggerEnter -= OnTargetEnteredAttackZone;

        private void OnTargetEnteredAttackZone(Collider target) => 
            _heroAnimator.SetAttackValue(true);
    }
}