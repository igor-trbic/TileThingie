using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 10f;

    // State
    bool isAlive = true;

    // Caching
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider2D;
    float startingGravity;

    // Messages then methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();

        startingGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Climb();
        FlipMe();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    private void Jump() {
        bool canJump = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && canJump) {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb() {
        bool canClimb = myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"));
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
}
