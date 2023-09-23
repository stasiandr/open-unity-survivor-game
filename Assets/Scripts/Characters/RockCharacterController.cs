using System;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    public class RockCharacterController : MonoBehaviour
    {
        [SerializeField, GetComponent] private Rigidbody rb;
        [SerializeField] private float speed;
        [SerializeField] private float rotationLerpAlpha;

        public Vector3ReactiveProperty Direction { get; } = new();

        private void FixedUpdate()
        {
            rb.velocity = Direction.Value * (Time.fixedDeltaTime * speed);
            
            if (Direction.Value == Vector3.zero) return;
            rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.LookRotation(Direction.Value), rotationLerpAlpha);
        }
    }
}