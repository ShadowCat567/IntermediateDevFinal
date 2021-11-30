using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public Transform playerTransform;
    public GameObject player;
    public GameObject sword;

    bool waveAttack;
    bool canUseSpecial = true;

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

        if (Input.GetKeyDown(KeyCode.F) && canUseSpecial)
        {
            //perform special ability
            StartCoroutine(WaveSetActive());
            WaveAttack();
            StartCoroutine(SpecialCooldown());
        }
    }

    public void LeftFacing()
    {
        if(!player.GetComponent<PlayerAttack>().facingLeft)
        {
            sword.transform.localPosition = new Vector3(0.6f, 0.1f, 0);
            player.GetComponent<PlayerAttack>().facingLeft = true;
        }
    }

    public void RightFacing()
    {
        if(player.GetComponent<PlayerAttack>().facingLeft)
        {
            sword.transform.localPosition = new Vector3(-0.6f, 0.1f, 0);
            player.GetComponent<PlayerAttack>().facingLeft = false;
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

    void WaveAttack()
    {
        while(waveAttack)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //execute wave attack
            }
        }
    }

    IEnumerator SpecialCooldown()
    {
        canUseSpecial = false;
        yield return new WaitForSeconds(1.5f);
        canUseSpecial = true;
    }

    IEnumerator colliderActive()
    {
        //activate the collider for 0.8 seconds to complete the attack
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }

    IEnumerator WaveSetActive()
    {
        waveAttack = true;
        yield return new WaitForSeconds(5.0f);
        waveAttack = false;
    }
}
