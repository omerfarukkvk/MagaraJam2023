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
    private bool isMoving = false;

    void Awake()
    {
        pickaxeTransform = transform.Find("Pickaxe");
        pickaxe = pickaxeTransform.gameObject;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(pickaxe.GetComponent<Pickaxe>().MiningCooldown());
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
            isMoving = true;
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            isMoving = false;
        }
    } 

    void Pickaxe()
    {
        pickaxe.SetActive(true);
        StartCoroutine(pickaxe.GetComponent<Pickaxe>().MiningCooldown());
        pickaxe.GetComponent<Pickaxe>().Mining();
    }
}

