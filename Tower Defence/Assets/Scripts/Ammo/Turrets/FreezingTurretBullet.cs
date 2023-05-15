using UnityEngine;

public class FreezingTurretBullet : BaseTurretBullet
{
    internal float FreezingTime { get; set; }
    internal float FreezingRatio { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyScript = other.GetComponent<BasicEnemy>();
            enemyScript.DivideSpeed(FreezingRatio, FreezingTime);
            enemyScript.health -= Damage;
        }
        Destroy(gameObject);
    }
}