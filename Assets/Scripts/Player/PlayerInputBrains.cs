using Characters;
using Contracts;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;
using Utilities;
using VContainer;

namespace Player
{
    [RequireComponent(typeof(RockCharacterController))]
    public class PlayerInputBrains : MonoBehaviour
    {
        [SerializeField] [GetComponent] private RockCharacterController controller;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            playerInput.Player.Move.ToObservable<Vector2>().Subscribe(val =>
            {
                if (val.magnitude > 1) val.Normalize();
                controller.SetDirection(val.ToWorld());
            }).AddTo(this);
        }
    }
}