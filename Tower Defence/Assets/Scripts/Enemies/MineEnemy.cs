using UnityEngine;

public class MineEnemy : BaseEnemy
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainTower")) return;
        if (TowerScript.Health - Damage <= 0)
            _script.enemyIsGone = true;
        TowerScript.GetDamage(Damage, gameObject.tag);
        Destroy(gameObject);

    }
}
