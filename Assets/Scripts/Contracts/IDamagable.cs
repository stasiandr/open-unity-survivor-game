namespace Global
{
    public interface IDamagable
    {
        void DealDamage(int damage);
    }
    
    /// <summary>
    /// This is just marker for collision handling to decide whether player or enemy
    /// </summary>
    public interface IPlayerHealth : IDamagable { }
    
    /// <summary>
    /// This is just marker for collision handling to decide whether player or enemy
    /// </summary>
    public interface IEnemyHealth : IDamagable { }
}