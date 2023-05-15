using System.Linq;
using UnityEngine;

public class BaseTurretBehavior : MonoBehaviour
{
    [SerializeField] internal int price = 100;
    [SerializeField] internal int damage = 25;
    [SerializeField] internal int range = 15;
    [SerializeField] internal float fireRate = 2f;
    [SerializeField] internal float accuracy;
    [SerializeField] protected string shootingPlaceTag = "ShootingPlace";
    internal Transform Target;
    public GameObject bulletPrefab;
    [SerializeField] internal string enemyTag = "Enemy";
    [SerializeField] internal float turnSpeed = 5f;
    internal Transform BulletShootPos;
    internal float _currentTime;
    [SerializeField] internal float bulletSpeed;

    private void Awake()
    {
        BulletShootPos = gameObject.GetComponentsInChildren<Transform>()
            .First(x => x.gameObject.CompareTag(shootingPlaceTag));
    }

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
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distance < shortestDist)) continue;
            shortestDist = distance;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDist <= range)
            Target = nearestEnemy.transform;
        else
            Target = null;
    }

    protected virtual void Rotate()
    {
        var dir = Target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    protected virtual void Shoot()
    {
        var firedBullet = Instantiate(bulletPrefab, BulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BaseTurretBullet>();
        SetBulletOptions(bulletScript);
    }

    protected virtual void CountTimeAndShoot()
    {
        if (_currentTime <= 0)
        {
            Shoot();
            _currentTime = fireRate;
        }

        _currentTime -= Time.deltaTime;
    }

    protected virtual void SetBulletOptions(BaseTurretBullet turretBulletScript)
    {
        turretBulletScript.Damage = damage;
        turretBulletScript.Target = Target;
        turretBulletScript.Speed = bulletSpeed;
    }
}