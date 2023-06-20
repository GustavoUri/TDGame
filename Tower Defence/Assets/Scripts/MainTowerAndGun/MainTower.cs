using System;
using Interfaces;
using UnityEngine;

public class MainTower : MonoBehaviour, IMainTower, IDamageable
{
    [SerializeField] private MainTowerHealthBar bar;
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int MaxHealth { get; private set; }
    public string lastHitter;
    private void Start()
    {
        bar.UpdateHealthBar(Health, MaxHealth);
    }

    public void GetDamage(int damage, string tagOfCaused)
    {
        Health -= damage;
        bar.UpdateHealthBar(Health, MaxHealth);
        lastHitter = tagOfCaused;
        // if(Health<=0)
        //     Destroy(gameObject);
    }

    public void Heal(int health)
    {
        Health += health;
        bar.UpdateHealthBar(Health, MaxHealth);
    }
}