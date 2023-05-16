using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainTower : MonoBehaviour
{
    [SerializeField] private MainTowerHealthBar bar;
    [SerializeField] internal int health = 100;
    [SerializeField] internal int maxHealth = 100;
    private void OnTriggerEnter(Collider other)
    {
        bar.UpdateHealthBar(health, maxHealth);
    }
}
