using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    private Vector2 velocity;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 使用 Dynamic 刚体并通过 velocity 驱动物理响应
        rb.isKinematic = false;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // 降低高速穿透

        // 我们维护一个独立的 velocity 字段，作为“权威值”
        velocity = new Vector2(0f, -1.0f);
        rb.velocity = velocity * 540;
    }

    // Reset logic removed: levels are instantiated fresh instead of resetting objects

    // 在物理引擎的碰撞回调里立即恢复我们的速度，防止引擎的碰撞响应改变它
    private void OnCollisionEnter2D(Collision2D coll)
    {
        rb.velocity = velocity * 540;
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        rb.velocity = velocity * 540;
    }

    public void OnCollideWall(int index) {
        var v = velocity;
        switch(index) {
            case 0:
                v.x = -v.x;
                break;
            case 1:
                v.y = -v.y;
                break;
            default:
                throw new System.Exception("unexpected wall index!");
        }
        velocity = v;
        rb.velocity = velocity * 540;
    }

    public void OnCollideRail(Vector2 railVelocity) {
        var v = velocity;
        v = new Vector2(railVelocity.x * 0.8f + v.x * 0.2f, -v.y);
        velocity = v;
        rb.velocity = velocity * 540;
    }
    public void OnCollideBlock(int index) {
        OnCollideWall(1 ^ index);
    }

}