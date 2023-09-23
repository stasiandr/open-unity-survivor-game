using UnityEngine;

namespace Global
{
    public interface IPlayerRouter
    {
        Transform PlayerBase { get; }
        Transform WeaponsContainer { get; }
    }
}