using UnityEngine;

public class MineEnemy : BasicEnemy
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainTower"))
        {
            TowerScript.health -= damage;
            Destroy(gameObject);
        }

    }
}
