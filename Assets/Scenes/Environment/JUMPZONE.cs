using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUMPZONE : MonoBehaviour
{
    // Start is called before the first frame update

    


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
            collision.gameObject.GetComponent<movement>().JumpZone();


        }
    }
   
}
