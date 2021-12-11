using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Playerspeed;// speed of the player
    public float Jumpforce;// player won't fly

    public Rigidbody2D rb;
    private float originalspeed;// Player's starting speed before speeding up
    public float launchforce;//player jumps when landing on the bouncezone

    void Start()
    {
        Playerspeed = 5f;
        rb = GetComponent<Rigidbody2D>();
        originalspeed = 5f;
        launchforce = 18;

    }

    // Update is called once per frame
    void Update()
    {

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * Playerspeed;
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, Jumpforce), ForceMode2D.Impulse);
        }
    }
   
    
   /* public void JumpZone()
    {

        // Player will bounce up when landing on the bouncezone
        rb.velocity = Vector2.up * launchforce;


    }*/

    public void Speedzone() {

        //The player will speed up when coming across speedzone
        Playerspeed = Playerspeed * 3;
    }
    public void Originalspeed()
    {
        //The player returns to the original speed after leaving the speedzone
        Playerspeed = originalspeed;
    }


}
