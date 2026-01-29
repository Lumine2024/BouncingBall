using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private int index;
    public GameManager gameManager;
    
    public int Index {
        get => index;
        set => index = value;
    }

    void Start() {
        var rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }
    // Reset logic removed: levels are re-instantiated instead
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<Ball>() == gameManager.GetBall()) {
            gameManager.OnBallCollideWall(coll);
        }
        //if (coll.gameObject.GetComponent<Ball>() != null)
        //{
        //    //coll.gameObject.GetComponent<Ball>().OnCollideWall(this);
        //}
    }

    // Reset logic removed: levels are re-instantiated instead
}
