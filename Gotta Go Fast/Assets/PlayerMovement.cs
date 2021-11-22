using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] public LayerMask platformsLayerMask;
    [SerializeField] float jumpVelocity;

    private float xSpeed = 0;
    private float ySpeed = 0;

    [SerializeField] float maxSpeed;
    [SerializeField] float acc;
    [SerializeField] float dec;

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
            rb2d.velocity = Vector2.up * jumpVelocity;
        }

        if ((Input.GetKey("left")) && (xSpeed > -maxSpeed))
        {
            xSpeed = xSpeed - acc * Time.deltaTime; 
        }

        else if ((Input.GetKey("right")) && (xSpeed < maxSpeed))
        {
            xSpeed = xSpeed + acc * Time.deltaTime; 
        }

        else
        {
            if (xSpeed > dec * Time.deltaTime) {
                xSpeed = xSpeed - dec * Time.deltaTime; 
            }

            else if(xSpeed < -dec * Time.deltaTime)
            {
                xSpeed = xSpeed + dec * Time.deltaTime; 
            }

            else
            {
                xSpeed = 0;
            }
        }

        transform.position += Vector3.right * xSpeed;  
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null; 
    }


}
