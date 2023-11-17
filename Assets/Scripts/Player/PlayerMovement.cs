using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb; 
    Vector2 movement;
    public Animator animator;

    // Pickaxe
    public GameObject pickaxe;
    public Transform pickaxePos;
    public Transform pickaxeTransform;
    private Coroutine miningCoroutine;

    // Sword
    public GameObject sword;
    public Transform swordPos;
    public Transform swordTransform;
    private Coroutine attackingCoroutine;
    private bool isMoving = false;

    void Awake()
    {
        pickaxePos = transform.Find("PickaxePos");
        pickaxeTransform = transform.Find("PickaxePos/Pickaxe");
        pickaxe = pickaxeTransform.gameObject;
        swordPos = transform.Find("SwordPos");
        swordTransform = transform.Find("SwordPos/Sword");
        sword = swordPos.gameObject;
    }

    void Start()
    {
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        PickaxeAnimPos();
        SwordAnimPos();

        if(movement.x != 0 || movement.y != 0)
        {
            isMoving = false;
        }

        KeyUpSpace();

        /*Debug.Log("isMining: " + pickaxe.GetComponent<Pickaxe>().isMining);
        Debug.Log("isMiningTrigger: " + pickaxe.GetComponent<Pickaxe>().isMiningTrigger);
        Debug.Log("isMoving: " + isMoving);
        */


    }

    void FixedUpdate()
    {
        KeyDownSpace();
        if (!pickaxe.GetComponent<Pickaxe>().isMining)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            isMoving = true;
        }
    }

    void PickaxeAnimPos()
    {
        if (movement.x < 0)
        {
            pickaxePos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pickaxePos.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            pickaxePos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            pickaxePos.localScale = new Vector3(1, 1, 1);
        }
    }
    void SwordAnimPos()
    {
        if (movement.x < 0)
        {
            swordPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            swordPos.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            swordPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            swordPos.localScale = new Vector3(1, 1, 1);
        }
    }

    void Pickaxe()
    {
        pickaxe.SetActive(true);
        miningCoroutine = StartCoroutine(pickaxe.GetComponent<Pickaxe>().MiningCooldown());
        pickaxe.GetComponent<Pickaxe>().Mining();
    }

    void Sword()
{
    if (sword == null)
    {
        Debug.LogError("Sword object is not assigned.");
        return;
    }

    Sword swordComponent = sword.GetComponent<Sword>();
    if (swordComponent == null)
    {
        Debug.LogError("Sword component is missing on the Sword object.");
        return;
    }

    if (swordComponent.isAttacking)
    {
        Debug.Log("Sword is already attacking.");
        return;
    }
    Debug.Log("Sword");
    sword.SetActive(true);
    attackingCoroutine = StartCoroutine(swordComponent.AttackingCooldown());
    swordComponent.Attacking();
}

    void KeyDownSpace()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (pickaxe.GetComponent<Pickaxe>().isMiningTrigger && isMoving)
            {
                Pickaxe();
            }
            else
                Sword();
        }
    }
    void KeyUpSpace()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (pickaxe.GetComponent<Pickaxe>() != null && pickaxe.GetComponent<Pickaxe>().isMining)
            {
                if (miningCoroutine != null)
                {
                    pickaxe.GetComponent<Pickaxe>().isMining = false;
                    StopCoroutine(miningCoroutine);
                }
                pickaxe.SetActive(false);
                return;
            }
            else if (sword.GetComponent<Sword>() != null && sword.GetComponent<Sword>().isAttacking)
            {
                if (attackingCoroutine != null)
                {
                    sword.GetComponent<Sword>().isAttacking = false;
                    StopCoroutine(attackingCoroutine);
                }
                sword.SetActive(false);
                return;
            }
        }
    }
}

