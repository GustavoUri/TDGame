using UnityEngine;

namespace Turrets.Interfaces
{
    public interface ITurret
    {
        int Price { get; }
        float ProjectileSpeed { get; }
        int ProjectileDamage { get; }
        int Range { get; }
        float FireRate { get; }
        string TargetTag { get; }
        
        float InstantiationRange { get; }
        GameObject InstantiationModel { get; }
    }
}