using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class FloatingEnemyHealthBar : BaseHealthBar
{
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera);
    }
}