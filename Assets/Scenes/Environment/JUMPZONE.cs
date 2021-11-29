using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUMPZONE : MonoBehaviour
{
    // Start is called before the first frame update

    float jumpForce = 60;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    // The player bounce up by calling the jump function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            rb2d.velocity += Vector2.up * jumpForce; 

        }
    }
   
}
