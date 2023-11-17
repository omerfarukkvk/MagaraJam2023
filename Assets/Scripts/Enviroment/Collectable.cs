using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int collectableMaxHeatlh = 100;
    public int collectableCurrentHeatlh;
    public GameObject collectable;
    void Start()
    {
        collectableCurrentHeatlh = collectableMaxHeatlh;
    }
    public void TakeDamage(int damage)
    {
        collectableCurrentHeatlh -= damage;
        if (collectableCurrentHeatlh <= 0)
        {
            DropItem();
            Die();
        }
    }

    void DropItem()
    {
        int random = Random.Range(0, 4);
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
