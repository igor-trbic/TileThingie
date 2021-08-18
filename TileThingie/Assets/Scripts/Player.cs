using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(-10f, 15f);

    // State
    bool isAlive = true;

    // Caching
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float startingGravity;

    // Messages then methods
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();

        startingGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update() {
        if (isAlive) {
            Run();
            Jump();
            Climb();
            FlipMe();
            Die();
        }
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    private void Jump() {
        bool canJump = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && canJump) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb() {
        bool canClimb = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        if (!canClimb) {
            myRigidbody.gravityScale = startingGravity;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        myRigidbody.gravityScale = 0;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(myRigidbody.velocity.x, controlThrow * climbSpeed);
        myRigidbody.velocity = playerVelocity;

        bool hasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", hasVerticalSpeed);
    }

    private void FlipMe() {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void Die() {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazzards"))) {
            myAnimator.SetTrigger("Die");
            isAlive = false;
            myRigidbody.velocity = deathKick;
        }
    }
}
