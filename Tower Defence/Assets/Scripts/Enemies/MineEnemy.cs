using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : BasicEnemy
{
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "MainTower")
            other.GetComponent<MainTower>().damageHP(damage);
            Destroy(gameObject);
        
    }
}
