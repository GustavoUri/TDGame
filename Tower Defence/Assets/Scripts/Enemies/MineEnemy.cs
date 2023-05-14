using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : BasicEnemy
{
    private void OnTriggerEnter(Collider other) {
            Destroy(gameObject);
        
    }
}
