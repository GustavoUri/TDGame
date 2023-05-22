using UnityEngine;

public class FreezingTurret : BaseTurret
{
    [field: SerializeField] public float FreezingTime { get; protected set; }
    [field: SerializeField] public float FreezingRatio { get; protected set; }

    protected override void Shoot()
    {
        var firedBullet = Instantiate(BulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<FreezingTurretProjectile>();
        bulletScript.InitializeProps(ProjectileSpeed, ProjectileDamage, Target, FreezingTime, FreezingRatio);
    }
}