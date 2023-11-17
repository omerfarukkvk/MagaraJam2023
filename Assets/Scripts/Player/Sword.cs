using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform swordPoint;
    public float swordRange = 0.5f; 
    public int swordDamage = 2;
    public bool isAttacking = false;

    void Awake()
    {
        //swordPoint = gameObject.transform.Find("Sword/SwordPoint");
    }
    void Update()
    {

    }

    public void Attacking()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(swordPoint.position, swordRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().TakeDamage(swordDamage);
            }
        }
    } 

    void OnDrawGizmosSelected()
    {
        if (swordPoint == null)
            return;
        Gizmos.DrawWireSphere(swordPoint.position, swordRange);
    }

    public IEnumerator AttackingCooldown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
}
