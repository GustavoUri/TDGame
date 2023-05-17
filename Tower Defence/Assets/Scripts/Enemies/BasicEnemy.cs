using System.Collections;
using Ammo.Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    protected NavMeshAgent NavMeshAgent;

    [SerializeField] protected int damage;
    [SerializeField] protected float speed = 5;
    [SerializeField] protected float speedAfter = 5;
    [SerializeField] internal int health = 100;
    [SerializeField] protected int stealHp;
    [SerializeField] protected int maxHealth = 100;
    protected bool IsStaggered;
    protected FloatingEnemyHealthBar Bar;
    public Transform tower;
    protected MainTower TowerScript { get; set; }
    [SerializeField] protected Vector3 endPosition;
    protected Vector3 FollowPosition;

    // Start is called before the first frame update
    void Start()
    {
        Bar = gameObject.GetComponentInChildren<FloatingEnemyHealthBar>();
        tower = GameObject.FindWithTag("MainTower").transform;
        TowerScript = tower.GetComponent<MainTower>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = speed;
        FollowPosition = tower.transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);
        NavMeshAgent.SetDestination(FollowPosition);
        DestroyOnDistance();
    }

    private IEnumerator Stagger(float divider, float time)
    {
        IsStaggered = true;
        NavMeshAgent.speed /= divider;
        yield return new WaitForSeconds(time);
        NavMeshAgent.speed *= divider;
        IsStaggered = false;
    }


    internal void DivideSpeed(float divider, float time)
    {
        if (IsStaggered) return;
        StartCoroutine(Stagger(divider, time));
    }

    private void death()
    {
        if (health <= 0)
        {
            ReturnHpToTower(stealHp); // Возращаем ХП
            Destroy(gameObject); // Умираем
        }
    }

    private void ReturnHpToTower(int stealedHp)
    {
        TowerScript.health += stealedHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainTower"))
        {
            СhangeSpeed(); //Меняем скорость, если этого не надо указываем speedAfter := speed
            TowerScript.health -= damage;
            if(TowerScript.health <= 0)
                Loader.Load(0, false);
            FollowPosition = endPosition; // Меняем поизицию на обратную
            stealHp += damage; // С кладываем сворованное хп
        }
        
        if (other.CompareTag("Bullet"))
        {
            var bulletScript = other.GetComponent<IBullet>();
            health -= bulletScript.Damage;
            Bar.UpdateHealthBar(health, maxHealth);
            if (health <= 0)
                Destroy(gameObject);
        }
    }

    protected virtual void СhangeSpeed()
    {
        NavMeshAgent.speed = speedAfter;
    }

    protected virtual void DestroyOnDistance()
    {
        if (Vector3.Distance(transform.position, endPosition) < 0.5 && FollowPosition == endPosition)
        {
            Destroy(gameObject);
        }
    }
}