using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Platform Mask; used to make sure the playr can only jump on platforms. 
    [SerializeField] public LayerMask platformsLayerMask;

    //The maximum speed the player can move. The value I used for testing is 60.
    [SerializeField] float maxSpeed;
    
    //How fast the player accelerates. The value I used for testing is 8.3. 
    [SerializeField] float acc;

    //How fast the player deccelerates when no horizontal input is detected. The value I used for testing is 3.53. 
    [SerializeField] float dec;

    //How powerful the jump is. The value I used for testing is 35. 
    [SerializeField] float jumpVelocity;

    public float xSpeed = 0;

    //If Unity yells at you about this variable don't worry about it; I haven't implemented a use for it just yet. 
    private float ySpeed = 0;

    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;



    void Start()
    {
        rb2d = transform.GetComponent<Rigidbody2D>();
        box2d = transform.GetComponent<BoxCollider2D>();
}

    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            //da jump
            rb2d.velocity += Vector2.up * jumpVelocity;
        }

        //Left movement.
        if ((Input.GetKey("left")) && (xSpeed > -maxSpeed))
        {
            //Transfers momentum if the player is already moving in a direction. Feels good, I think. 
            if(xSpeed > -maxSpeed/4)
            {
                xSpeed = xSpeed - dec; 
            }

            xSpeed = xSpeed - acc * Time.deltaTime; 
        }

        //Right movement, same as before. 
        else if ((Input.GetKey("right")) && (xSpeed < maxSpeed))
        {
            if (xSpeed < maxSpeed/4)
            {
                xSpeed = xSpeed + dec;
            }

            xSpeed = xSpeed + acc * Time.deltaTime; 
        }

        //Deceleration happens when neither right nor left is pressed, slowing the player based on how high the dec variable is.
        else
        {

            if (xSpeed > 0) {
                xSpeed = xSpeed - dec;
            }

            else if(xSpeed < -dec)
            {
                xSpeed = xSpeed + dec; 
            }

            //Safety net
            else
            {
                xSpeed = 0;
            }
        }

        //Debug.Log("xspeed: " + xSpeed);
        rb2d.velocity = new Vector2(xSpeed, rb2d.velocity.y);  
    }

    //Detects for ground underneath player. Might adjust the offset so the player can jump while being farther away from the ground? Will look into it. 
    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null; 
    }


}
