using System;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationController : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        [SerializeField, GetComponent] private Animator animator;
        [SerializeField, GetComponentInParent] private RockCharacterController controller;

        private void Start()
        {
            controller.Direction
                .Select(v => v.magnitude)
                .Subscribe(s => animator.SetFloat(Speed, s))
                .AddTo(this);
        }
    }
}