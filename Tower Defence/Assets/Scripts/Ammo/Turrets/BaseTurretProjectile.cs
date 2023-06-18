using Ammo.Interfaces;
using Interfaces;
using UnityEngine;

public class BaseTurretProjectile : MonoBehaviour, IProjectile
{
    public float Speed { get; protected set; }
    public int Damage { get; protected set; }
    public Transform Target { get; protected set; }
    protected Vector3 TargetPosition { get; set; }

    public GameObject bulletEffect;

    public void InitializeProps(float speed, int damage, Transform target)
    {
        Speed = speed;
        Damage = damage;
        Target = target;
    }


    private void Update()
    {
        if (Target == null) Destroy(gameObject);
        else
        {
            TargetPosition = Target.position;
            var dir = TargetPosition - transform.position;
            Move(dir);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var otherDamageable = other.GetComponent<IDamageable>();
        otherDamageable?.GetDamage(Damage, gameObject.tag);

        GameObject effect = (GameObject)(Instantiate(bulletEffect,transform.position,transform.rotation));
        effect.GetComponent<Renderer>().material.color = other.GetComponent<Renderer>().material.color;
        Destroy(effect,1f);

        Destroy(gameObject);
    }

    protected virtual void Move(Vector3 dir)
    {
        transform.Translate(dir.normalized * (Speed * Time.deltaTime), Space.World);
    }
}