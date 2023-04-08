using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 target;
    public float speed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var distanceThisFrame = speed * Time.deltaTime;
        var dir = target - transform.position;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
