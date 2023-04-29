using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private int health = 100;
    private bool _isStaggered;
    public Transform tower;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var towerPos = tower.position;
        var dist = Vector3.Distance(towerPos, pos);
        if (!(dist > 10)) return;
        var diff = towerPos - pos;
        transform.Translate(diff * (Time.deltaTime * speed), Space.World);
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Bullet"))
    //         health -= other.GetComponent<BulletMoving>().damage;
    //     //Debug.Log(health);
    //     if (health <= 0)
    //         Destroy(gameObject);
    // }z

    internal void DivideSpeed(float divider)
    {
        if (_isStaggered) return;
        _isStaggered = true;
        speed /= divider;
    }
}