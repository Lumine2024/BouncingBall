using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    public GameManager gameManager;
    
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<Ball>() == gameManager.GetBall()) {
            gameManager.OnBallOutofBound(coll);
        }
    }

    // Reset logic removed: levels are re-instantiated instead
}
