using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 2.0f; 

    Vector2 direction;
    float playerSpeed;
    public bool MovingLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = GameObject.Find("Sword").transform.localPosition;
        playerSpeed = GameObject.Find("player").GetComponent<PlayerMovement>().xSpeed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().enemyHealth -= 2;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * (playerSpeed * speed), ForceMode2D.Impulse);
    }
}
