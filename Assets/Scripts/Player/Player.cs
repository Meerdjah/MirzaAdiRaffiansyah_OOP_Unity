using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    PlayerMovement playerMovement;
    Animator animator;

    //Method Awake() digunakan untuk membuat singleton dari Player pada saat GameObject pertama kali diinstansiasi
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Method Start() digunakan untuk memuat component dan script lain di Player ke dalam variabel
    void Start()
    {
        //Mengambil informasi dari script PlayerMovemet.cs
        playerMovement = GetComponent<PlayerMovement>();

        //Mencari GameObject EngineEffect dan mengambil informasi component animator dari EngineEffect
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning(this + "tidak ada EngineEffect");
        }
    }

    // Menjalankan method Move dari playerMovement dengan frekuensi tertentu untuk menggerakkan karakter
    void FixedUpdate()
    {
        if (playerMovement != null)
        {
            playerMovement.Move();
        }
    }

    // Menjalankan method setBool dari animator setelah method FixedUpdate() untuk menjalankan animasi
    void LateUpdate()
    {
        if (animator != null && playerMovement != null)
        {
            playerMovement.MoveBound();
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
