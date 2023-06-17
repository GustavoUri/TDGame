using System;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _mouseLastPosition;
    [SerializeField] private Texture2D scopeTexture2D;
    public float ScopePositionX { get; private set; }
    public float ScopePositionY { get; private set; }
    [SerializeField] private float sensitivity;
    [SerializeField] private float scopeWidth;
    [SerializeField] private float scopeHeight;

    // private void Start()
    // {
    //     SetScopePosition(Screen.width / 2, Screen.height / 2);
    // }

    // Update is called once per frame
    void Update()
    {
        GetScopePosition();
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(ScopePositionX, ScopePositionY, scopeWidth, scopeHeight), scopeTexture2D);
    }

    private void GetScopePosition()
    {
        var posX = Input.GetAxis("Mouse X");
        var posY = Input.GetAxis("Mouse Y");
        ScopePositionX += posX * sensitivity;
        ScopePositionY -= posY * sensitivity;
        ScopePositionY = Mathf.Clamp(ScopePositionY, 0, Screen.height);
        ScopePositionX = Mathf.Clamp(ScopePositionX, 0, Screen.width);
    }

    public void SetScopePosition(float x, float y)
    {
        ScopePositionX = x;
        ScopePositionY = y;
    }
}