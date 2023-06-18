using Interfaces;
using UnityEngine;

public class FreezingTurretProjectile : BaseTurretProjectile
{
    public float FreezingTime { get; protected set; }
    public float FreezingRatio { get; protected set; }

    private void OnTriggerEnter(Collider other)
    {
        var otherDamageable = other.GetComponent<IDamageable>();
        var otherFreezable = other.GetComponent<IFreezable>();
        otherDamageable?.GetDamage(Damage, gameObject.tag);
        otherFreezable?.GetFreeze(FreezingTime, FreezingRatio, gameObject.tag);

        GameObject effect = (GameObject)(Instantiate(bulletEffect,transform.position,transform.rotation));
        Destroy(effect,1f);

        Destroy(gameObject);
    }

    public void InitializeProps(float speed, int damage, Transform target, float freezingTime, float freezingRatio)
    {
        Speed = speed;
        Damage = damage;
        Target = target;
        FreezingRatio = freezingRatio;
        FreezingTime = freezingTime;
    }
}