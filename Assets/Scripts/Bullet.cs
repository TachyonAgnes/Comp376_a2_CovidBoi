using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float mSpeed;
    Rigidbody2D mRigidBody2D;

    void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();

        // Set a default direction
        mRigidBody2D.velocity = Vector2.zero;
    }

    public void SetDirection(Vector2 direction)
    {
        mRigidBody2D.velocity = direction * mSpeed;
    }
}
