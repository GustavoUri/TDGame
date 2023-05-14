using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicEnemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; 
    
    [SerializeField] private int damage; 
    [SerializeField] private float speed = 5;
    [SerializeField]private float speedAfter = 5;
    [SerializeField] private int health = 100;
    [SerializeField]private int stealHp = 0;
    private bool _isStaggered;
    public Transform tower;
    [SerializeField]private Transform  endPosition;
    private Transform followPosition;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        followPosition = tower.transform;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(followPosition.position);
    }

    public void damageHP(int damage){
        health -=damage;
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
        //Debug.Log("ds");
        if (_isStaggered) return;
        StartCoroutine(Stagger(divider, time));
    }
    private void death(){
        if (health <=0 ){
            returnHP(stealHp); // Возращаем ХП
            Destroy(gameObject); // Умираем
        }
    }
    private void returnHP(int stealHp){
        tower.GetComponent<MainTower>().addHP(stealHp); // Возращаем ХП
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("RUUUN");
        if (other.tag == "MainTower"){
            Debug.Log("RUUUN");
            changeSpeed(); //Меняем скорость, если этого не надо указываем speedAfter := speed
            other.GetComponent<MainTower>().damageHP(damage); // Наносим урон по центру 
            followPosition = endPosition;// Меняем поизицию на обратную
            stealHp += damage; // С кладываем сворованное хп
        }
    }
    public void changeSpeed(){
        navMeshAgent.speed = speedAfter;
    }
}