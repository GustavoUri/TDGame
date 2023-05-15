using Ammo.Interfaces;
using UnityEngine;

public class BaseMainGunBullet : MonoBehaviour, IBullet
{
    public float Speed { get; set; }
    public int Damage { get; set; }
    internal Vector3 TargetPosition { get; set; }
    private Transform _transform;
    protected Vector3 Direction { get; set; }
    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        Direction = TargetPosition - _transform.position;
    }


    private void Update()
    {
        Move(Direction);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void Move(Vector3 dir)
    {
        _transform.Translate(dir.normalized * (Speed * Time.deltaTime), Space.World);
    }
}