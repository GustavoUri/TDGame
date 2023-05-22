using Interfaces;
using UnityEngine;

public class ArtilleryTurretProjectile : BaseTurretProjectile
{
    public float ExplosionRange { get; protected set; }
    [SerializeField] private string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance >= ExplosionRange) continue;
            var enemyScript = enemy.GetComponent<IDamageable>();
            enemyScript.GetDamage(Damage, gameObject.tag);
        }

        Destroy(gameObject);
    }

    public void InitializeProps(float speed, int damage, Transform target, float explosionRange)
    {
        Speed = speed;
        Damage = damage;
        Target = target;
        ExplosionRange = explosionRange;
    }
}