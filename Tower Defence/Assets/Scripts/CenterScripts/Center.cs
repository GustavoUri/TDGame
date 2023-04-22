using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    [SerializeField]private int hp;

    public void damageHP(int damage){
        hp -=damage;
    }
}
