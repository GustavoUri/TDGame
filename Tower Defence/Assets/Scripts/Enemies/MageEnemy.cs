using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MageEnemy : BasicEnemy
{
    private float distanceToTower;
    [SerializeField] private int plusStealHP;
    [SerializeField]private int stealHpForSecond;
    private MainTower mainTower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
        mainTower = tower.GetComponent<MainTower>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        followPosition = tower.transform.position;
        endPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        StartCoroutine(stealHP());
    }
    IEnumerator stealHP(){
        this.stealHp +=stealHpForSecond;
        mainTower.damageHP(stealHpForSecond);
        yield return new WaitForSeconds(1);
        StartCoroutine(stealHP());
    }
}
