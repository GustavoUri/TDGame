using Interfaces;
using UnityEngine;

public class BaseMainGun : MonoBehaviour, IMainGun
{
    [field:SerializeField] public bool IsMainGunRotating { get; protected set; }
    public GameObject bulletPrefab;
    [SerializeField] private Transform bulletShootPos;
    private RaycastHit _hit;
    [field: SerializeField] public float ProjectileSpeed { get; protected set; }
    [field: SerializeField] public int ProjectileDamage { get; protected set;}
    [field: SerializeField] public int Price { get; protected set;}
    public Vector3 Target { get; protected set; }

    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            IsMainGunRotating = !IsMainGunRotating;
        }

        if (IsMainGunRotating)
            return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _hit))
        {
            Target = _hit.point;
        }


        Rotate();
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }

    void Rotate()
    {
        if (Time.deltaTime == 0) return;
        var dir = Target - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = lookRotation.eulerAngles;
        var parTrans = gameObject.GetComponentInParent<Transform>();
        parTrans.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }

    void Shoot()
    {
        Debug.Log("Ass");
        if (Time.deltaTime == 0) return;
        var firedBullet = Instantiate(bulletPrefab, bulletShootPos.transform.position, transform.rotation);
        var bulletScript = firedBullet.GetComponent<BaseMainGunProjectile>();
        //firedBullet.transform.SetParent(transform);
        bulletScript.InitializeProps(ProjectileSpeed, ProjectileDamage, Target);
    }
}