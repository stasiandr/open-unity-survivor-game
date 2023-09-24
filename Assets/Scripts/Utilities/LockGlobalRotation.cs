using UnityEngine;

namespace Utilities
{
    public class LockGlobalRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 eulers;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(eulers);
        }
    }
}