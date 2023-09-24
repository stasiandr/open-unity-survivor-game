using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Global
{
    public static class ProjectUtilities
    {
        public static Vector3 ToWorld(this Vector2 vec)
        {
            return new Vector3(vec.x, 0, vec.y);
        }

        public static IObservable<InputAction.CallbackContext> ToObservable(this InputAction action)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                h =>
                {
                    action.performed += h;
                    action.canceled += h;
                },
                h =>
                {
                    action.performed -= h;
                    action.canceled -= h;
                });
        }

        public static IObservable<T> ToObservable<T>(this InputAction action) where T : struct
        {
            return action.ToObservable().Select(c => c.ReadValue<T>());
        }

        public static IEnumerable<int> CumulativeSum(this IEnumerable<int> sequence)
        {
            var sum = 0;
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum;
            }
        }

        public static T GetRandom<T>(this IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            var length = enumerable.Length;
            return enumerable[Random.Range(0, length)];
        }
    }
}