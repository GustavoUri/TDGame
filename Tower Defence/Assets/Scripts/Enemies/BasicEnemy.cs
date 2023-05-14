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
    {/*
        var pos = transform.position;
        var towerPos = tower.position;
        var dist = Vector3.Distance(towerPos, pos);
        if (!(dist > 10)) return;
        var diff = towerPos - pos;
        transform.Translate(diff * (Time.deltaTime * speed), Space.World);
    */
        navMeshAgent.SetDestination(followPosition.position);
    }

    public void damageHP(int damage){
        health -=damage;
    }

    internal void DivideSpeed(float divider)
    {
        if (_isStaggered) return;
        _isStaggered = true;
        speed /= divider;
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