using UnityEngine;

namespace FXOnly
{
    public class AlwaysLookUp : MonoBehaviour
    {
        [SerializeField] private Vector3 eulers;
        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(eulers);
        }
    }
}