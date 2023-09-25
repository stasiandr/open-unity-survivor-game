using Contracts;
using UnityEngine;

namespace Player
{
    public class PlayerRouter : MonoBehaviour, IPlayerRouter
    {
        [field: SerializeField] public Transform PlayerBase { get; private set; }

        [field: SerializeField] public Transform WeaponsContainer { get; private set; }
    }
}