using UnityEngine;

public class MineEnemy : BaseEnemy
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainTower"))
        {
            TowerScript.GetDamage(Damage, gameObject.tag);
            Destroy(gameObject);
        }

    }
}
