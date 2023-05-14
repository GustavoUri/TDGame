using UnityEngine;
using UnityEngine.AI;
public class BasicEnemy : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent; 
    
    [SerializeField] protected int damage; 
    [SerializeField] protected float speed = 5;
    [SerializeField]protected float speedAfter = 5;
    [SerializeField] protected int health = 100;
    [SerializeField]protected int stealHp = 0;
    protected bool _isStaggered;
    public Transform tower;
    [SerializeField]protected Vector3  endPosition;
    protected Vector3 followPosition;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        followPosition = tower.transform.position;
        endPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
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
        navMeshAgent.SetDestination(followPosition);
        isRunAway();
    }

    public virtual void damageHP(int damage){
        health -=damage;
    }

    internal virtual void DivideSpeed(float divider)
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
        if (other.tag == "MainTower"){
            changeSpeed(); //Меняем скорость, если этого не надо указываем speedAfter := speed
            other.GetComponent<MainTower>().damageHP(damage); // Наносим урон по центру 
            followPosition = endPosition;// Меняем поизицию на обратную
            stealHp += damage; // С кладываем сворованное хп
        }
    }
    public void changeSpeed(){
        navMeshAgent.speed = speedAfter;
    }
    private void isRunAway(){
        if (Vector3.Distance(transform.position,endPosition) < 0.5 && followPosition == endPosition){
            Destroy(gameObject);
        }
    }
}