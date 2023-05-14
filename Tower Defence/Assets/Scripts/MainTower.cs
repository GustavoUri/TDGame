using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    public int health = 100;
    
    public void damageHP(int damage){
        health -=damage;
        Debug.Log("hp:"+health);
    }
    public void addHP(int addHP){
        health +=addHP;
    }

}
