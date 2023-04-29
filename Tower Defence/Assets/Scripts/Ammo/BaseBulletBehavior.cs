using UnityEngine;

public class BaseBulletBehavior : MonoBehaviour
{
    internal float Speed { get; set; }
    internal int Damage { get; set; }
    internal Transform Target { get; set; }
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }


    private void Update()
    {
        if (Target == null) Destroy(gameObject);
        var position = Target.position;
        var dir = position - _transform.position;
        Move(dir);
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