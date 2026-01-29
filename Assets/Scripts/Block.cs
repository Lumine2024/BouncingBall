using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int index = -1;
    public GameManager gameManager;
    public bool destroyed = false;
    public int Index {
        get {
            if (index == -1) throw new System.Exception("The block is not collided yet!");
            return index;
        }
    }
    public bool Destroyed { get { return destroyed; } }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (destroyed) return;
        if(coll.gameObject.GetComponent<Ball>() == gameManager.GetBall()) {
            var contact = coll.GetContact(0);
            var norm = contact.normal;
            index = Mathf.Abs(norm.x) > Mathf.Abs(norm.y) ? 1 : 0;
            gameManager.OnBallCollideBlock(coll);
        }
    }

    // Reset logic removed: the level will be re-instantiated instead of resetting blocks
}
