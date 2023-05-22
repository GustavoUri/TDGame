using Ammo.Interfaces;
using Interfaces;
using UnityEngine;

public class BaseMainGunProjectile : MonoBehaviour, IProjectile
{
    public float Speed { get; private set; }
    public int Damage { get; private set; }
    public Vector3 TargetPosition { get; private set; }
    private Vector3 Direction { get; set; }

    public virtual void InitializeProps(float speed, int damage, Vector3 targetPosition)
    {
        Speed = speed;
        Damage = damage;
        TargetPosition = targetPosition;
    }

    private void Start()
    {
        
        Direction = TargetPosition - transform.position;
    }


    private void Update()
    {
        Move(Direction);
    }


    private void OnTriggerEnter(Collider other)
    {
        var otherDamageable = other.GetComponent<IDamageable>();
        otherDamageable?.GetDamage(Damage, gameObject.tag);
        Destroy(gameObject);
    }

    protected virtual void Move(Vector3 dir)
    {
        transform.Translate(dir.normalized * (Speed * Time.deltaTime), Space.World);
    }
}