using UnityEngine;

public class FreezingTurret : BaseTurretBehavior
{
    [SerializeField] private float freezingTime;
    [SerializeField] private float freezingRatio;

    protected override void Shoot()
    {
        var firedBullet = Instantiate(bulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<FreezingBullet>();
        SetBulletOptions(bulletScript);
    }

    private void SetBulletOptions(FreezingBullet bulletScript)
    {
        bulletScript.Damage = damage;
        bulletScript.Target = Target;
        bulletScript.Speed = bulletSpeed;
        bulletScript.FreezingRatio = freezingRatio;
        bulletScript.FreezingTime = freezingTime;
    }
}