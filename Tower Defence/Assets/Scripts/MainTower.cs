using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    public int health = 100;
    
    public void damageHP(int damage){
        health -=damage;
    }
    public void addHP(int addHP){
        health +=addHP;
    }
    void OnTriggerEnter(Collider col){
        Debug.Log("RUUUN");
    }
}
