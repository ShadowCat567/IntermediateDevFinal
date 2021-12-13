using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1.5f;

    [SerializeField] Animator projectileAnim;

    float projectileSpeed = 5.0f;
    Vector2 direction;
    float playerSpeed;
    public bool MovingLeft;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(GameObject.Find("Sword").transform.localPosition.x, -GameObject.Find("Sword").transform.localPosition.y);
        playerSpeed = GameObject.Find("player").GetComponent<PlayerMovement>().xSpeed; 
    }

    private void Start()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerSpeed = (playerSpeed) * -1;
            projectileAnim.Play("WavePorjectileLeft");
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            projectileAnim.Play("WaveProjectileRight");
        }
    
        if (playerSpeed == 0)
        {
            playerSpeed = projectileSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().enemyHealth -= 0.2f;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(direction * (playerSpeed * speed), ForceMode2D.Impulse);
    }
}
