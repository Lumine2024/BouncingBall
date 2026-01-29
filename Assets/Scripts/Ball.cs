using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float DefaultSpeed = 540f;

    [SerializeField]
    private float speed = DefaultSpeed;
    [SerializeField]
    private float maxBounceAngle = 75f;
    [SerializeField]
    private float railVelocityInfluence = 0.35f;

    Rigidbody2D rb;
    private Vector2 velocity;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ʹ�� Dynamic ���岢ͨ�� velocity ����������Ӧ
        rb.isKinematic = false;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // ���͸��ٴ�͸

        // ����ά��һ�������� velocity �ֶΣ���Ϊ��Ȩ��ֵ��
        velocity = new Vector2(0f, -1.0f);
        ApplyVelocity();
    }

    // Reset logic removed: levels are instantiated fresh instead of resetting objects

    // �������������ײ�ص��������ָ����ǵ��ٶȣ���ֹ�������ײ��Ӧ�ı���
    private void OnCollisionEnter2D(Collision2D coll)
    {
        ApplyVelocity();
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        ApplyVelocity();
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
        SetVelocity(v);
    }

    public void OnCollideRail(Collision2D coll, Vector2 railVelocity) {
        var railCollider = coll.collider;
        var contact = coll.GetContact(0);
        var railBounds = railCollider.bounds;
        var halfWidth = railBounds.size.x / 2f;
        var offset = halfWidth > Mathf.Epsilon
            ? (contact.point.x - railBounds.center.x) / halfWidth
            : 0f;
        offset = Mathf.Clamp(offset + railVelocity.x * railVelocityInfluence, -1f, 1f);
        var angle = offset * maxBounceAngle * Mathf.Deg2Rad;
        var v = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        SetVelocity(v);
    }
    public void OnCollideBlock(int index) {
        OnCollideWall(1 ^ index);
    }

    private void ApplyVelocity()
    {
        rb.velocity = velocity.normalized * speed;
    }

    private void SetVelocity(Vector2 newVelocity)
    {
        if (newVelocity == Vector2.zero) {
            return;
        }

        velocity = newVelocity.normalized;
        ApplyVelocity();
    }
}
