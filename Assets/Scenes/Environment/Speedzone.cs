using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPEEDZONE : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // if the player comes across the platform the speed increase by calling the speedzone function
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<movement>().Speedzone();
        }
    }
    // if the player leave the platform the speed decrease to original speed
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<movement>().Originalspeed();


        }
    }

}
