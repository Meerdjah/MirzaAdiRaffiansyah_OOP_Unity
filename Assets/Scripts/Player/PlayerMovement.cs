using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;
    [SerializeField] Vector2 offset;
    [SerializeField] Camera mainCamera;
    Vector2 moveDirection;
    Vector2 moveVelocity;
    Vector2 moveFriction;
    Vector2 stopFriction;
    Rigidbody2D rb;
    Vector2 screenBounds;

    /*
    Method Start() digunakan untuk memuat component di Player ke dalam variabel serta 
    melakukan kalkulasi awal untuk moveVelocity, moveFriction, dan stopFriction
    */
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning(this + "tidak ada Rigidbody2D");
        }

        //melakukan kalkulasi awal untuk moveVelocity, moveFriction, dan stopFriction
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
        //Debug.Log("moveVelocity: " + moveVelocity + "moveFriction: " + moveFriction + "stopFriction: " + stopFriction);

        //Menginisialisasi batas awal kamera
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
    }

    //Method untuk menggerakkan karakter berdasarkan input player
    public void Move()
    {
        //Mengubah input player menjadi data Vector2
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //Menghitung kecepatan karakter serta melakukan clamp agar kecepatan tidak melebihi batas
        rb.velocity += (moveDirection * moveVelocity * maxSpeed  + GetFriction()) * Time.fixedDeltaTime;
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        //Menghentikan pergerakan karakter jika kecepatannya lebih kecil dari stopClamp
        if (moveDirection.magnitude == 0)
        {
            if (Mathf.Abs(rb.velocity.x) < stopClamp.x) 
                rb.velocity = new Vector2(0, rb.velocity.y);
            if (Mathf.Abs(rb.velocity.y) < stopClamp.y) 
                rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        //Debug.Log("velocityX: " + rb.velocity.x + "velocityY: " + rb.velocity.y);
        //Debug.Log("friction: " + GetFriction());
    }

    //Method untuk menghitung gaya gesek untukk karakter
    public Vector2 GetFriction()
    {
        //Mengembalikan friction dengan menggunakan moveFriction jika terdapat player input dan menggunakan stopFriction jika sebaliknya
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

    //Method Untuk memberikan batas agar player tidak dapat bergerak keluar layar
    public void MoveBound()
    {
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, -(screenBounds.x - offset.x), screenBounds.x - offset.x),
            Mathf.Clamp(rb.position.y, -(screenBounds.y), screenBounds.y - offset.y)
        );
    }

    //Method yang digunakan untuk mengembalikan nilai true jika karakter bergerak dan sebaliknya
    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }
}
