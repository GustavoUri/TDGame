using UnityEngine;

public class ImprovedEnemyBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private int health = 100;
    private float fireRate = 3f;
    private float countDown = 3f;
    public Transform tower;

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindWithTag("MainTower").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = tower.position - transform.position;
        var dist = Vector3.Distance(tower.position, transform.position);
        Debug.Log(tower.position);
        if (dist > 10)
            transform.Translate(pos * (Time.deltaTime * speed), Space.World);
        //if (tower == null) return;
        if (countDown <= 0)
        {
            Shoot();
            countDown = fireRate;
        }
        
        countDown -= Time.deltaTime;
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Bullet"))
    //         health -= other.GetComponent<BulletMoving>().damage;
    //     //Debug.Log(health);
    //     if (health <= 0)
    //         Destroy(gameObject);
    // }
    
    void Shoot()
    {
        var firedBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BaseTurretProjectile>();
        firedBullet.transform.SetParent(transform);
    }
}