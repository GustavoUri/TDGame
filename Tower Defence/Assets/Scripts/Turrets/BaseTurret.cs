using System.Linq;
using Turrets.Interfaces;
using UnityEngine;

public class BaseTurret : MonoBehaviour, ITurret
{
    [field: SerializeField] public Transform Target { get; protected set; }
    [field: SerializeField] public GameObject BulletPrefab { get; protected set; }
    [field: SerializeField] public float TurnSpeed { get; protected set; }
    [field: SerializeField] public Transform BulletShootPos { get; protected set; }
    private float _timeBeforeShoot;
    [field: SerializeField] public int Price { get; protected set; }
    [field: SerializeField] public float ProjectileSpeed { get; protected set; }
    [field: SerializeField] public int ProjectileDamage { get; protected set; }
    [field: SerializeField] public int Range { get; protected set; }
    [field: SerializeField] public float FireRate { get; protected set; }
    [field: SerializeField] public string TargetTag { get; protected set; }

    [field: SerializeField] public float InstantiationRange { get; protected set; }

    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void Update()
    {
        if (Target == null) return;
        Rotate();
        CountTimeAndShoot();
    }

    protected virtual void UpdateTarget()
    {
        var shortestDist = Mathf.Infinity;
        var enemies = GameObject.FindGameObjectsWithTag(TargetTag);
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distance < shortestDist)) continue;
            shortestDist = distance;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDist <= Range)
            Target = nearestEnemy.transform;
        else
            Target = null;
    }


    protected virtual void Rotate()
    {
        var dir = Target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }


    protected virtual void Shoot()
    {
        var firedBullet = Instantiate(BulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BaseTurretProjectile>();
        bulletScript.InitializeProps(ProjectileSpeed, ProjectileDamage, Target);
    }

    protected virtual void CountTimeAndShoot()
    {
        if (_timeBeforeShoot <= 0)
        {
            Shoot();
            _timeBeforeShoot = FireRate;
        }

        _timeBeforeShoot -= Time.deltaTime;
    }
}