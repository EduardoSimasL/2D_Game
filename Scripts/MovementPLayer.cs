using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPLayer : MonoBehaviour
{
    public float speed;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    Vector2 movement;
    public Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
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
        if(movement != Vector2.zero) {
            bool success = TryMove(movement);

            if(!success) {
                success = TryMove(new Vector2(movement.x, 0));

                if(!success) {
                    success = TryMove(new Vector2(0, movement.y));
                }
            }
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

    private bool TryMove(Vector2 movement) {
        // Check for potential collisions
            int count = rb.Cast(
                movement, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                speed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movement = movementValue.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("swordAttack");
    }
}
