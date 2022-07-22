using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SheepController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private float firstRangePointPosition;
    [SerializeField] private float secondRangePointPosition;
    [Range(0, 1000)] [SerializeField] private float maxRotationSpeed;
    [Range(0, 100)] [SerializeField] private float rotateSpeed;
    [Range(0, 100)] [SerializeField] private float moveSpeed;
    [Range(0, 100)] [SerializeField] private float maxSpeed;

    public int flag = 1;

    private Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveInRange();
    }
    private void MoveInRange()
    {
        if (Math.Abs(rigidbody2D.angularVelocity) < maxRotationSpeed)
        {
            rigidbody2D.AddTorque(flag * -rotateSpeed * 2);
        }
        if (rigidbody2D.velocity.magnitude < maxSpeed)
        {
            rigidbody2D.AddForce(new Vector2(flag * moveSpeed, 0), ForceMode2D.Force);
        }

        if (transform.localPosition.x <= firstRangePointPosition) 
        {
            flag = 1;
            rigidbody2D.velocity -= Vector2.left * Time.fixedDeltaTime * 20;
        }
        if (transform.localPosition.x >= secondRangePointPosition) 
        {
            flag = -1;
            rigidbody2D.velocity += Vector2.left * Time.fixedDeltaTime * 20;
        }
    }
}
