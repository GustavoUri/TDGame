using System.Linq;
using UnityEngine;

public class BaseMainGun : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int price;
    public bool isMainGunRotating;
    public Vector3 target;
    public float turnSpeed = 5f;
    public GameObject bulletPrefab;
    private Transform _bulletShootPos;
    private RaycastHit _hit;
    private void Start()
    {
        _bulletShootPos = gameObject.GetComponentsInChildren<Transform>()
            .First(x => x.gameObject.CompareTag("ShootingPlace"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            isMainGunRotating = !isMainGunRotating;
        }

        if (isMainGunRotating)
            return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit))
        {
            target = _hit.point;
        }
        

        Rotate();
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    void Rotate()
    {
        var dir = target - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = lookRotation.eulerAngles;
        var parTrans = gameObject.GetComponentInParent<Transform>();
        parTrans.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
    
    void Shoot()
    {
        var firedBullet = Instantiate(bulletPrefab, _bulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BaseMainGunBullet>();
        firedBullet.transform.SetParent(transform);
        bulletScript.TargetPosition = target;
        bulletScript.Damage = damage;
        bulletScript.Speed = speed;
        Debug.Log(_hit.transform);
    }
}