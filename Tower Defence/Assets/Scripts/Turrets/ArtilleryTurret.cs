using UnityEngine;

public class ArtilleryTurret : BaseTurret
{
    [field:SerializeField] public float ExplosionRange { get; protected set; }
    
    protected override void Shoot()
    {
        var firedBullet = Instantiate(BulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<ArtilleryTurretProjectile>();
        bulletScript.InitializeProps(ProjectileSpeed, ProjectileDamage, Target, ExplosionRange);
    }
}