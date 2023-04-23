using DG.Tweening;
using UnityEngine;

namespace Scripts.Logic.Enemy
{
    [RequireComponent(typeof(CharacterDeath))]
    public class DeathFlyOut : MonoBehaviour
    {
        [SerializeField] private int _forceX = 50;
        [SerializeField] private int _forceY = 10;
        [SerializeField] private float _duration = 1f;
        private CharacterDeath _death;

        private void Awake() => 
            _death = GetComponent<CharacterDeath>();

        private void OnEnable() => 
            _death.Dead += FlyOut;

        private void OnDisable() => 
            _death.Dead -= FlyOut;

        private void FlyOut() =>
            transform.DOMove(Movement, _duration).SetEase(Ease.InOutSine);

        private Vector3 Movement => MovementX + MovementY;
        private Vector3 MovementX => transform.position - transform.forward * _forceX;
        private Vector3 MovementY => Vector3.up * _forceY;
    }
}
