using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{

    [SerializeField] private Dictionary<string, int> inventory = new Dictionary<string, int>();
    public string[] itemsNames;
    void Start()
    {
        AddDictionaryInventory();
    }

    void Update()
    {
        
    }
    void AddDictionaryInventory()
    {
        for (int i = 0; i < itemsNames.Length; i++)
        {
            inventory.Add(itemsNames[i], 0);
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
