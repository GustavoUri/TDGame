using System;
using System.Collections;
using Enemies.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour, IEnemy, IDamageable, IFreezable
{
    protected NavMeshAgent NavMeshAgent;

    [field: SerializeField] public int Health { get; protected set; }
    [field: SerializeField] public int Damage { get; protected set; }
    [field: SerializeField] public float Speed { get; protected set; }
    [field: SerializeField] public float SpeedAfter { get; protected set; }
    public int StealedHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; protected set; }
    protected bool IsStaggered;
    protected FloatingEnemyHealthBar Bar;
    [field: SerializeField] public GameObject Tower { get; protected set; }
    protected MainTower TowerScript { get; set; }
    [field: SerializeField] protected Vector3 EndPosition { get; set; }
    protected Vector3 FollowPosition;
    [SerializeField] private GameObject endManager;
    protected LvlEndManager _script;
    [SerializeField] private int healthBonus;

    private bool _theChosenOne;
    // Start is called before the first frame update
    private void Awake()
    {
        endManager = GameObject.Find("LevelEndUI");
        _script = endManager.GetComponent<LvlEndManager>();
        Bar = gameObject.GetComponentInChildren<FloatingEnemyHealthBar>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.speed = Speed;
    }

    private void Start()
    {
        Tower = GameObject.FindWithTag("MainTower");
        TowerScript = Tower.GetComponent<MainTower>();
        FollowPosition = Tower.transform.position;
        var position = transform.position;
        EndPosition = new Vector3(position.x, position.y, position.z);
    }

    // Update is called once per frame
    private void Update()
    {
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


    protected virtual void DivideSpeed(float divider, float time)
    {
        if (IsStaggered) return;
        StartCoroutine(Stagger(divider, time));
    }

    private void CheckDeath()
    {
        if (Health > 0) return;
        TowerScript.Heal(StealedHealth + healthBonus);
        Destroy(gameObject); // Умираем
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainTower"))
        {
            СhangeSpeed(); //Меняем скорость, если этого не надо указываем speedAfter := speed
            if (TowerScript.Health - Damage <= 0)
                _theChosenOne = true;
            TowerScript.GetDamage(Damage, gameObject.tag);
            FollowPosition = EndPosition; // Меняем поизицию на обратную
            StealedHealth += Damage; // С кладываем сворованное хп
        }
    }

    protected virtual void СhangeSpeed()
    {
        NavMeshAgent.speed = SpeedAfter;
    }

    protected virtual void DestroyOnDistance()
    {
        var dist = Vector3.Distance(transform.position, EndPosition);
        //Debug.Log(dist);
        if (dist < 1f && FollowPosition == EndPosition)
        {
            if (_theChosenOne)
                _script.enemyIsGone = true;
            Destroy(gameObject);
        }
    }

    public void GetDamage(int damage, string tagOfCaused)
    {
        Health -= damage;
        Bar.UpdateHealthBar(Health, MaxHealth);
        CheckDeath();
    }

    public void GetFreeze(float freezingTime, float freezingRatio, string freezerTag)
    {
        DivideSpeed(freezingRatio, freezingTime);
    }
}