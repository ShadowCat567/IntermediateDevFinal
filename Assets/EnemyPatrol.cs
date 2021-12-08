using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class EnemyPatrol : MonoBehaviour
{
    //wandering
    //make sure enemy has rigidbody, collider, and trigger collider for damaging player
    //enemy patrol script

    //create enemy empty child, wallcheck, drag wallcheck into enemy patrol script and set radius
    //create edge check empty child as well
=======
//  enemy
//wandering
//make sure enemy has rigidbody, collider, and trigger collider for damaging player
//enemy patrol script

//create enemy empty child, wallcheck, drag wallcheck into enemy patrol script and set radius
//create edge check empty child as well

public class EnemyPatrol : MonoBehaviour
{
>>>>>>> Stashed changes

    public float moveSpeed;
    public bool moveRight; //whether he is moving left or right

    //values for making enemy turn 
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool notAtEdge;
    public Transform edgeCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        //in update

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;

        //adds basic enemy movement
        if (moveRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); //flips the enemy and wallcheck 
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        //makes enemy turn around when hitting a wall
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
    }
}
