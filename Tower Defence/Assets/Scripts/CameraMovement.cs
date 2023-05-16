using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float mouseBuffer = 10f;
    [SerializeField] private int minY = 10;
    [SerializeField] private int maxY = 100;
    [SerializeField] private float scrollSpeed = 5f;
    public bool isOnSpace;
    [SerializeField] private int minX = -100;
    [SerializeField] private int maxX = 100;
    [SerializeField] private int minZ = -100;
    [SerializeField] private float turnSpeed = 10;
    public bool isOnAbove; 
    [SerializeField] private int maxZ = 100;
    private bool _isRotDone;

    [SerializeField] private GameObject turretsUI;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            _isRotDone = false;
            isOnSpace = !isOnSpace;
        }

        if (Input.mousePosition.y >= Screen.height - mouseBuffer)
        {
            Move(Vector3.forward);
        }

        if (Input.mousePosition.y <= mouseBuffer)
        {
            Move(Vector3.back);
        }

        if (Input.mousePosition.x >= Screen.width - mouseBuffer)
        {
            
            
            Move(Vector3.right);
        }

        if (Input.mousePosition.x <= mouseBuffer)
        {
            Move(Vector3.left);
        }

        switch (isOnSpace)
        {
            case true when _isRotDone == false:
                MakeViewForTurretsInstantiating();
                break;
            case false when _isRotDone == false:
                MakeViewForTargeting();
                break;
        }

        // if (isOnSpace && !_isRotDone)
        // {
        //     MakeViewFromAbove();
        // }
        //
        // if (!isOnSpace && !_isRotDone)
        // {
        //     MakeViewForTargeting();
        // }

        var scroll = Input.GetAxis("Mouse ScrollWheel");
        var pos = transform.position;
        pos.y -= scroll * Time.deltaTime * scrollSpeed;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }

    void Move(Vector3 direction)
    {
        transform.Translate(direction * (Time.deltaTime * speed), Space.World);
    }

    void MakeViewForTurretsInstantiating()
    {
        turretsUI.SetActive(true);
        isOnAbove = true;
        var newRot = Quaternion.Euler(new Vector3(90, 0, 0));
        var rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
        var pos = transform.position;
        pos.y = 25;
        transform.position = pos;
        if (Math.Abs(transform.rotation.eulerAngles.x - 90) < 0.25f)
        {
            _isRotDone = true;
        }
    }

    void MakeViewForTargeting()
    {
        turretsUI.SetActive(false);
        isOnAbove = false;
        var newRot = Quaternion.Euler(new Vector3(45, 0, 0));
        var rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, 0, 0);
        var pos = transform.position;
        pos.y = 25;
        transform.position = pos;
        if (Math.Abs(transform.rotation.eulerAngles.x - 45) < 0.25f)
        {
            _isRotDone = true;
        }
    }
}