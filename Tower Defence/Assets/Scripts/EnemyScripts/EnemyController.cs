using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private int damage;
    [SerializeField]private Transform  CenterPosition;
    
    [SerializeField]private Transform  endPosition;

    private Transform followPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        followPosition = CenterPosition;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(followPosition.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Center"){
            
            other.GetComponent<Center>().damageHP(damage);
            followPosition = endPosition;
        }
    }
}
