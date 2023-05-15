using UnityEngine;

public class FreezingTurret : BaseTurretBehavior
{
    [SerializeField] private float freezingTime;
    [SerializeField] private float freezingRatio;

    protected override void Shoot()
    {
        var firedBullet = Instantiate(bulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<FreezingTurretBullet>();
        SetBulletOptions(bulletScript);
    }

    private void SetBulletOptions(FreezingTurretBullet turretBulletScript)
    {
        turretBulletScript.Damage = damage;
        turretBulletScript.Target = Target;
        turretBulletScript.Speed = bulletSpeed;
        turretBulletScript.FreezingRatio = freezingRatio;
        turretBulletScript.FreezingTime = freezingTime;
    }
}