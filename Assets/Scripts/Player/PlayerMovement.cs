using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
        Debug.Log("moveVelocity: " + moveVelocity + "moveFriction: " + moveFriction + "stopFriction: " + stopFriction);
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        rb.velocity += (moveDirection * moveVelocity * maxSpeed  + GetFriction()) * Time.fixedDeltaTime;

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        if (moveDirection.magnitude == 0){
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x) 
                rb.velocity = new Vector2(0, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < stopClamp.y) 
                rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        Debug.Log("velocityX: " + rb.velocity.x + "velocityY: " + rb.velocity.y);
        Debug.Log("friction: " + GetFriction());
    }

    public Vector2 GetFriction() {
    if (moveDirection.magnitude > 0)
        {
            return new Vector2(
                moveFriction.x * rb.velocity.x,
                moveFriction.y * rb.velocity.y
            );
        }
        else
        {
            return new Vector2(
                stopFriction.x * rb.velocity.x,
                stopFriction.y * rb.velocity.y
            );
        }
    }



    public void MoveBound()
    {
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }
}
