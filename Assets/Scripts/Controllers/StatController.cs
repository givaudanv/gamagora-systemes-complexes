using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0) Die();
    }

    public void Damage(int dmg)
    {
        currentHealth -= dmg;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
