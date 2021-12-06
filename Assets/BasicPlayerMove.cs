using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed = 5.0f;
    [SerializeField] float moveX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void PlayerControls()
    { 
        //player movement
        moveX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    private void FixedUpdate()
    {
        //movement stuff
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }
}
