using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class MovementPLayer : MonoBehaviour
{
    public float speed;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    Vector2 movement;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            spriteRenderer.flipX = movement.x < 0;
    }
       void FixedUpdate()
       {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

            /* animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

            rb.velocity = new Vector2 (moveHorizontal, moveVertical);
            spriteRenderer.flipX = rb.velocity.x < 0;

            if (moveVertical > 0) {
                animator.Play("player_run_back");
            } else if (moveVertical < 0) {
                animator.Play("player_front");
            } */

       } 
}
