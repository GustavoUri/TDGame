using UnityEngine;

namespace Interfaces
{
    public interface IMainGun
    {
        float ProjectileSpeed { get; }
        int ProjectileDamage { get; }
        int Price { get; }
        Vector3 Target { get; }
    }
}