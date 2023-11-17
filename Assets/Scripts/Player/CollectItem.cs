using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    Dictionary<string, int> inventory = new Dictionary<string, int>();
    public string[] itemsNames;
    public int[] itemsQuantity;
    void Start()
    {
        AddDictionaryInventory();
    }

    void Update()
    {
        
    }
    void AddDictionaryInventory()
    {
        SetItemQuantity();
        for (int i = 0; i < itemsNames.Length; i++)
        {
            inventory.Add(itemsNames[i], itemsQuantity[i]);
        }
    }

    void SetItemQuantity()
    {
        itemsQuantity = new int[itemsNames.Length];
        for (int i = 0; i < itemsNames.Length; i++)
        {
            itemsQuantity[i] = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "StoneItem")
        {
            inventory[other.tag]++;
            Destroy(other.gameObject);
        }
    }
}
