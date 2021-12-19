using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //      collision.gameObject.GetComponent<>().playerHealth -= 0.1f;
            collision.gameObject.GetComponent<PlayerHealth>().playerHealth -= 0.25f;
        }
    }
}
