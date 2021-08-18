using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 3f;

    Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFacingRight()) {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        } else {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

    private bool isFacingRight() {
        return transform.localScale.x > 0;
    }
}
