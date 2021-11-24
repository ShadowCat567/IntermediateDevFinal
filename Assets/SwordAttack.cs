using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //when left mouse button is pressed, attack
            StartCoroutine(colliderActive());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F))
        {
            //perform special ability
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if sword collides with an enemy, make the enemy take one damage
            collision.GetComponent<EnemyHealth>().enemyHealth -= 1;
        }
    }

    IEnumerator colliderActive()
    {
        //activate the collider for 0.8 seconds to complete the attack
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }
}
