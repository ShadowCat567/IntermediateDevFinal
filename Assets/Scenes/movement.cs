using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Playerspeed;
    public float Jumpforce;

    public Rigidbody2D rb;
    private float originalspeed;
    public float launchforce;

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "speedzone")
        {
            Debug.Log("fk");
            //Playerspeed = Playerspeed * 3;

        }
    }
    
    public void JumpZone()
    {


        rb.velocity = Vector2.up * launchforce;


    }
    public void Speedzone() {


        Playerspeed = Playerspeed * 3;
    }
    public void Originalspeed()
    {
        Playerspeed = originalspeed;
    }


}
