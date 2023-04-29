using System;
using UnityEngine;

public class ArtilleryBullet : BaseBulletBehavior
{
    [SerializeField] private float explosionRange;
    [SerializeField] private string enemyTag = "Enemy";
    private void OnTriggerEnter(Collider other)
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var enemy in enemies)
        {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distance >= explosionRange)) continue;
            Debug.Log(enemy.tag + "Boom");
            Destroy(gameObject);
        }
    }
}