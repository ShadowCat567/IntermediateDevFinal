using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer sr;

    public GameObject player;
    
    bool hittingEnemy;

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
            Debug.Log("Hitting Enemy");
            hittingEnemy = true;
            collision.GetComponent<EnemyBehavior>().EnemyStunned();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Block());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject targetEnemy = EnemyInDetectDistance();
            if (targetEnemy)
            {
                StartCoroutine(SheildBash());
                player.GetComponent<BasicPlayerMove>().shieldDash = true;
                player.GetComponent<BasicPlayerMove>().SpecialAttack(targetEnemy);
            }

            //perform special ability
            //if enemy is within a certain range, dash to the enemy -- how to get the enemy that is in that range
            //dash distance = distance to enemy, this would probably more flexible...just need to get the detection down
            //really short range dash, shield becomes solid and temporarily stuns emeny (stun means enemy cannot attack or move)
        }
    }

    GameObject EnemyInDetectDistance()
    {
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

    IEnumerator Block()
    {
        boxCollider.enabled = true;
        boxCollider.isTrigger = false;
        sr.color = new Color(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        boxCollider.isTrigger = true;
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }

    IEnumerator SheildBash()
    {
        boxCollider.enabled = true;
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(0.3f);
        boxCollider.enabled = false;
    }
}
