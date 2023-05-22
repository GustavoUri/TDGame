using UnityEngine;

namespace Ammo.Interfaces
{
    public interface IProjectile
    {
        int Damage { get; }
        float Speed { get; }
    }
}