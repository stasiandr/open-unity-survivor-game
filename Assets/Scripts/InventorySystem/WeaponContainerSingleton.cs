using UnityEngine;

namespace InventorySystem
{
    [DefaultExecutionOrder(-10000)]
    public class WeaponContainerSingleton : MonoBehaviour // TODO Remove with DI
    {
        public static WeaponContainerSingleton Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}