using System;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class RockCharacterController : MonoBehaviour
    {
        [SerializeField] [GetComponent] private Rigidbody rb;
        [SerializeField] private float speed = 200;
        [SerializeField] private float rotationLerpAlpha = 0.2f;

        public IReadOnlyReactiveProperty<Vector3> Direction => _direction;
        private readonly Vector3ReactiveProperty _direction = new();

        private void FixedUpdate()
        {
            rb.velocity = _direction.Value * (Time.fixedDeltaTime * speed);

            if (_direction.Value == Vector3.zero) return;
            rb.rotation = Quaternion.Lerp(
                rb.rotation,
                Quaternion.LookRotation(_direction.Value),
                rotationLerpAlpha);
        }

        public void SetDirection(Vector3 newDirection)
        {
            _direction.Value = newDirection;
        }
    }
}