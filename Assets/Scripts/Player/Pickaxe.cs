using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private Transform pickaxePoint;
    public float pickaxeRange = 0.5f; 
    public int pickaxeDamage = 2;
    public bool isMining = false;

    void Awake()
    {
        pickaxePoint = GetComponentInChildren<Transform>();
    }
    void Update()
    {
        
    }

    public void Mining()
    {
        Collider2D[] hitCollectable = Physics2D.OverlapCircleAll(pickaxePoint.position, pickaxeRange);
        foreach (Collider2D collectable in hitCollectable)
        {
            if (collectable.tag == "Collectable")
            {
                collectable.GetComponent<Collectable>().TakeDamage(pickaxeDamage);
            }
        }
    } 

    void OnDrawGizmosSelected()
    {
        if (pickaxePoint == null)
            return;
        Gizmos.DrawWireSphere(pickaxePoint.position, pickaxeRange);
    }

    public IEnumerator MiningCooldown()
    {
        isMining = true;
        yield return new WaitForSeconds(1.2f);
        isMining = false;
    }
}
