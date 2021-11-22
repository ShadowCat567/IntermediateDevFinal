using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if left mouse botton is pressed, execute attack
            StartCoroutine(colliderActive());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if your weapon collides with an enemy, take health from the enemy
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().enemyHealth -= 1;
        }
    }

    IEnumerator colliderActive()
    {
        //enable/disable weapon collider
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }
}
