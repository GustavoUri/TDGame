using UnityEngine;

public class FreezingBullet : BaseBulletBehavior
{
    internal float FreezingTime { get; set; }
    internal float FreezingRatio { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyScript = other.GetComponent<BasicEnemy>();
            enemyScript.DivideSpeed(FreezingRatio);
        }
        Destroy(gameObject);
    }
}