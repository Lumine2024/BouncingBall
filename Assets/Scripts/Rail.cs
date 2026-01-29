using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    private Vector2 velocity;
    public GameManager gameManager;

    

    public Vector2 Velocity {
        get => velocity;
    }

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            // do nothing
        } else if(Input.GetKey(KeyCode.A)) {
            velocity.x = Mathf.Max(-1.0f, velocity.x + -1.0f * Time.deltaTime);
            //velocity = Vector2.left;
        } else if(Input.GetKey(KeyCode.D)) {
            velocity.x = Mathf.Min(1.0f, velocity.x + 1.0f * Time.deltaTime);
            //velocity = Vector2.right;
        } else {
            velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime * 540);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<Ball>() == gameManager.GetBall()) {
            gameManager.OnBallCollideRail(coll);
        }
        //if(coll.gameObject.GetComponent<Ball>() != null) {
        //    //coll.gameObject.GetComponent<Ball>().OnCollideRail(this);
        //}
    }

    // Reset logic removed: levels are re-instantiated instead
}
