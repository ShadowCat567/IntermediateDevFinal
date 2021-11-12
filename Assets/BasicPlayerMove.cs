using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float moveX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void PlayerControls()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }
}
