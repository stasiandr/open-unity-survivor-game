namespace InventorySystem
{
    public struct InitializationData
    {
        public InitializationData(int level)
        {
            Level = level;
        }

        public int Level { get; set; }
    }
}