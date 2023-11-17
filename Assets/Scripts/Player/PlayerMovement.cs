using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb; 
    Vector2 movement;
    public Animator animator;
    public Transform pickaxeTransform;
    public GameObject pickaxe;
    public Transform pickaxePos;
    private bool isMoving = false;
    private Coroutine miningCoroutine;

    void Awake()
    {
        pickaxePos = transform.Find("PickaxePos");
        pickaxeTransform = transform.Find("PickaxePos/Pickaxe");
        pickaxe = pickaxeTransform.gameObject;
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

        if(movement.x != 0 || movement.y != 0)
        {
            isMoving = false;
        }
        
        PickaxeAnimPos();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (miningCoroutine != null)
            {
                pickaxe.GetComponent<Pickaxe>().isMining = false;
                StopCoroutine(miningCoroutine);
            }
            pickaxe.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && !pickaxe.GetComponent<Pickaxe>().isMining && !isMoving)
        {
            Pickaxe();
        }
        if (!isMoving && !pickaxe.GetComponent<Pickaxe>().isMining)
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

    void Pickaxe()
    {
        pickaxe.SetActive(true);
        miningCoroutine = StartCoroutine(pickaxe.GetComponent<Pickaxe>().MiningCooldown());
        pickaxe.GetComponent<Pickaxe>().Mining();
    }
}

