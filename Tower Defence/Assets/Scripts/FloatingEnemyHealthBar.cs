using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class FloatingEnemyHealthBar : BaseHealthBar
{
    private Transform _camera;

    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main.transform;
    }
    
    private void LateUpdate()
    {
        transform.LookAt(_camera);
    }
}