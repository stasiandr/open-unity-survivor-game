using UnityEngine;

namespace Contracts
{
    public interface IPlayerRouter
    {
        Transform PlayerBase { get; }
        Transform WeaponsContainer { get; }
    }
}