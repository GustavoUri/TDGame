using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class MageEnemy : BasicEnemy
{
    private float _distanceToTower;
    [SerializeField] private int plusStealHP;
    [SerializeField]private int stealHpForSecond;
    private MainTower _mainTower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
        _mainTower = tower.GetComponent<MainTower>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = speed;
        FollowPosition = tower.transform.position;
        endPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        StartCoroutine(StealHp());
    }
    IEnumerator StealHp(){
        this.stealHp +=stealHpForSecond;
        _mainTower.health-=stealHp;
        yield return new WaitForSeconds(1);
        StartCoroutine(StealHp());
    }
}
