using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //  enemy
    //wandering
    //make sure enemy has rigidbody, collider, and trigger collider for damaging player
    //enemy patrol script

    //create enemy empty child, wallcheck, drag wallcheck into enemy patrol script and set radius
    //create edge check empty child as well

    public float moveSpeed;
    public bool moveRight; //whether he is moving left or right

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(collisiion2D, collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveRight = !moveRight;
        }
    }

        // Update is called once per frame
        void Update()
        {

            //adds basic enemy movement
            if (moveRight)
            {
                transform.localScale = new Vector3(-1f, 1f, 0f); //flips the enemy and wallcheck 
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 0f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }

            //makes enemy turn around when hitting a wall
        }
}
