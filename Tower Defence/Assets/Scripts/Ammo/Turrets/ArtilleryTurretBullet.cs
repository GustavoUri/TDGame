using UnityEngine;

public class ArtilleryTurretBullet : BaseTurretBullet
{
    [SerializeField] private float explosionRange;
    [SerializeField] private string enemyTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distance >= explosionRange)) continue;
            var enemyScript = other.GetComponent<BasicEnemy>();
            enemyScript.health -= Damage;
        }

        Destroy(gameObject);
    }
}