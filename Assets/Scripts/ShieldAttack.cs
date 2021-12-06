using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer sr;

    public GameObject player;
    
    bool hittingEnemy;
    bool canUseSpecial = true;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //if the shield is colliding with the enemy, the enemy is stunned
            hittingEnemy = true;
            collision.GetComponent<EnemyBehavior>().EnemyStunned();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //when left mouse botton is pressed, the shield blocks
            StartCoroutine(Block());
        }

        if (Input.GetKeyDown(KeyCode.F) && canUseSpecial)
        {
            //finds a targetEnemy and moves to that targetEnemy, stunning it
            GameObject targetEnemy = EnemyInDetectDistance();
            if (targetEnemy)
            {
                StartCoroutine(SheildBash());
                player.GetComponent<BasicPlayerMove>().shieldDash = true;
                player.GetComponent<BasicPlayerMove>().SpecialAttack(targetEnemy);
            }

            StartCoroutine(SpecialCooldown());
            //perform special ability
            //if enemy is within a certain range, dash to the enemy -- how to get the enemy that is in that range
            //dash distance = distance to enemy, this would probably more flexible...just need to get the detection down
            //really short range dash, shield becomes solid and temporarily stuns emeny (stun means enemy cannot attack or move)
        }
    }

    GameObject EnemyInDetectDistance()
    {
        //Finds an enemy to target that is within 1.5f of the player, targetEnemy is the closest enemy
        GameObject targetEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 1.5f;

        for (int i = 0; i < enemies.Length; i ++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);

            if(distance < closest)
            {
                closest = distance;
                targetEnemy = enemies[i];
            }
        }
        return targetEnemy;
    }

    IEnumerator SpecialCooldown()
    {
        canUseSpecial = false;
        yield return new WaitForSeconds(1.5f);
        canUseSpecial = true;
    }

    IEnumerator Block()
    {
        //activates the sheild's collider for 0.5 seconds to block incoming attacks
        boxCollider.enabled = true;
        boxCollider.isTrigger = false;
        sr.color = new Color(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        boxCollider.isTrigger = true;
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }

    IEnumerator SheildBash()
    {
        //activates the sheild's collider when it is excuting the shield bash
        boxCollider.enabled = true;
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(0.3f);
        boxCollider.enabled = false;
    }
}
