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

    //values for making enemy turn 
   // public Transform wallCheck;
  //  public float wallCheckRadius;
   // public LayerMask whatIsWall;
    private bool hittingWall;

/*    private bool notAtEdge;
    public Transform edgeCheck;
*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //in update

      //  hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
//        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

      //  if (hittingWall)
        //    moveRight = !moveRight;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            moveRight = !moveRight;
        }
    }
}
