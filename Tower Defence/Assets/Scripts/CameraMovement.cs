using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0.01f;
    public float mouseBuffer = 10f;
    public int minY = 10;
    public int maxY = 100;
    public float scrollSpeed = 5f;
    private bool _isOnEscape;

    public int minX = -100;
    public int maxX = 100;
    public int minZ = -100;
    public int maxZ = 100;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            _isOnEscape = !_isOnEscape;
        if (_isOnEscape)
            return;
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
}