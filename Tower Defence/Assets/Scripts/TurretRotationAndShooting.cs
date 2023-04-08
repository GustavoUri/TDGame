using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretRotationAndShooting : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public float range = 15f;
    public string enemyTag = "Enemy";
    private float countDown = 0f;
    public float turnSpeed = 5f;
    public float fireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Rotate();
            if (countDown <= 0)
            {
                Shoot();
                countDown = fireRate;
            }

            countDown -= Time.deltaTime;
        }


    }

    void UpdateTarget()
    {
        var shortestDist = Mathf.Infinity;
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position,enemy.transform.position);
            if (distance < shortestDist)
            {
                shortestDist = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDist <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        
    }

    void Rotate()
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        var bulletShootPos = GameObject.FindWithTag("ShootingPlace");
        var firedBullet = Instantiate(bulletPrefab, bulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BulletMoving>();
        bulletScript.target = target.position;
    }
}
