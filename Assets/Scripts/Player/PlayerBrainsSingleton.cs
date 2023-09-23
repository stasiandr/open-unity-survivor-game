using Characters;
using Interfaces;
using Interfaces.Extensions;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;
using VContainer;

namespace Player
{
    [RequireComponent(typeof(RockCharacterController))]
    public class PlayerBrainsSingleton : MonoBehaviour
    {
        [SerializeField, GetComponent] private RockCharacterController controller;

        [Inject]
        public void Construct(PlayerInput playerInput)
        {
            playerInput.Player.Move.ToObservable<Vector2>().Subscribe(val =>
            {
                if (val.magnitude > 1) val.Normalize();
                controller.Direction.Value = val.ToWorld();
            }).AddTo(this);
        }
    }
}