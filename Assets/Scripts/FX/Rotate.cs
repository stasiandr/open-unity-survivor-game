using UnityEngine;

namespace FX
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Vector3 eulersVector;

        private Quaternion _startRotation;

        private void Awake()
        {
            _startRotation = transform.rotation;
        }

        private void LateUpdate()
        {
            transform.rotation = _startRotation * Quaternion.Euler(eulersVector * Time.time);
        }
    }
}