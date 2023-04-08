using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGunMouseTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isShooting;
    public Vector3 target;
    public float _turnSpeed = 5f;
    Plane plane = new Plane(Vector3.up, 0);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("g"))
            isShooting = !isShooting;
        if(isShooting)
            return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out var distance))
        {
            target = ray.GetPoint(distance);
        }
        Rotate();
    }
    
    void Rotate()
    {
        var dir = target - transform.position; 
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = lookRotation.eulerAngles;
        var parTrans = gameObject.GetComponentInParent<Transform>();
        parTrans.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
