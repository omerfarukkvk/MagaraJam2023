using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int enemyMaxHeatlh = 100;
    public int enemyCurrentHeatlh;
    public Slider enemyHealthBar;
    [SerializeField] EnemyHealth enemyHealth;

    void Awake()
    {
        enemyHealth = GetComponentInChildren<EnemyHealth>();
    }

    void Start()
    {
        enemyCurrentHeatlh = enemyMaxHeatlh;
        enemyHealth.UpdateHealthBar(enemyCurrentHeatlh);
    }
    public void TakeDamage(int damage)
    {
        enemyCurrentHeatlh -= damage;
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealth.UpdateHealthBar(enemyCurrentHeatlh); 
        if (enemyCurrentHeatlh <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}