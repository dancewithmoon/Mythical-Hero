using UnityEngine;

namespace Scripts.Logic
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private CharacterController _characterController;

        private void Awake() => 
            _characterController = GetComponent<CharacterController>();

        private void Update() => 
            _characterController.Move(Physics.gravity + MovementVector);

        private Vector3 MovementVector => Vector3.right * (_speed * Time.deltaTime);
    }
}
