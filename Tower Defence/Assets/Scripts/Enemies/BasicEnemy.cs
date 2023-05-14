using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;

    [SerializeField] protected int damage;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float speedAfter = 5;
    [SerializeField] protected int health = 100;
    [SerializeField] protected int stealHp = 0;
    protected bool _isStaggered;
    public Transform tower;
    [SerializeField] protected Vector3 endPosition;
    protected Vector3 followPosition;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        followPosition = tower.transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(followPosition);
        isRunAway();
    }

    public virtual void damageHP(int damage)
    {
        health -= damage;
    }

    private IEnumerator Stagger(float divider, float time)
    {
        _isStaggered = true;
        navMeshAgent.speed /= divider;
        yield return new WaitForSeconds(time);
        navMeshAgent.speed *= divider;
        _isStaggered = false;
    }


    internal void DivideSpeed(float divider, float time)
    {
        if (_isStaggered) return;
        StartCoroutine(Stagger(divider, time));
    }

    private void death()
    {
        if (health <= 0)
        {
            returnHP(stealHp); // Возращаем ХП
            Destroy(gameObject); // Умираем
        }
    }

    private void returnHP(int stealHp)
    {
        tower.GetComponent<MainTower>().addHP(stealHp); // Возращаем ХП
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainTower")
        {
            changeSpeed(); //Меняем скорость, если этого не надо указываем speedAfter := speed
            other.GetComponent<MainTower>().damageHP(damage); // Наносим урон по центру 
            followPosition = endPosition; // Меняем поизицию на обратную
            stealHp += damage; // С кладываем сворованное хп
        }
    }

    public void changeSpeed()
    {
        navMeshAgent.speed = speedAfter;
    }

    private void isRunAway()
    {
        if (Vector3.Distance(transform.position, endPosition) < 0.5 && followPosition == endPosition)
        {
            Destroy(gameObject);
        }
    }
}