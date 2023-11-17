using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private Transform pickaxePoint;
    public float pickaxeRange = 0.5f; 
    public bool isMining = false;

    void Start()
    {
        pickaxePoint = GameObject.Find("PickaxePoint").GetComponent<Transform>();
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
                collectable.GetComponent<Collectable>().TakeDamage(20);
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
        yield return new WaitForSeconds(1f);
        isMining = false;
    }
}
