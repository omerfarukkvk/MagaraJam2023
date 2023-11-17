using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public int collectableMaxHeatlh = 100;
    public int collectableCurrentHeatlh;
    public GameObject collectable;
    public Slider collectableHealthBar;
    [SerializeField] CollectableHealth collectableHealth;

    void Awake()
    {
        collectableHealth = GetComponentInChildren<CollectableHealth>();
    }

    void Start()
    {
        collectableCurrentHeatlh = collectableMaxHeatlh;
        collectableHealth.UpdateHealthBar(collectableCurrentHeatlh);
    }
    public void TakeDamage(int damage)
    {
        collectableCurrentHeatlh -= damage;
        collectableHealthBar.gameObject.SetActive(true);
        collectableHealth.UpdateHealthBar(collectableCurrentHeatlh); 
        if (collectableCurrentHeatlh <= 0)
        {
            DropItem();
            Die();
        }
    }

    void DropItem()
    {
        int random = Random.Range(1, 4);
        Vector3 pos = transform.position;
        for (int i = 0; i < random; i++, pos.x += 0.1f)
        {
            Instantiate(collectable, pos, Quaternion.identity);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
