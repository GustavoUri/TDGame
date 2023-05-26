using UnityEngine;

public class MineEnemy : BaseEnemy
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MainTower")) return;
        TowerScript.GetDamage(Damage, gameObject.tag);
        Destroy(gameObject);

    }
}
