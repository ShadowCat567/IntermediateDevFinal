using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolearmAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public GameObject player;
    public GameObject spear;
    public GameObject spearCore;

    float speed = 1000.0f;

    //spear movement related variables
    bool spearThrown = false;
    //distance spear can be thrown
    float throwDistance = 20.0f;
    //position spear should move to
    Vector3 movePos;
    float playerX;
    //target x position
    float targetX;
    //next x position
    float nextX;
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
            if(spearThrown)
            {
                //when F key is pressed and spearThrown is true, player moves to spear
                StartCoroutine(SpearPosition());
                StartCoroutine(SpecialCooldown());
            }

            else
            {
                //when F key is pressed and spearThrown is false, spear is thrown and spearThrown is set to true
                ThrowSpear();
                spear.transform.SetParent(null);
                spearThrown = true;
            }
        }
    }

    public void FacingLeft()
    {
        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            spear.transform.localPosition = new Vector3(0.9f, 0, 0);
            player.GetComponent<PlayerAttack>().facingLeft = true;
        }
    }

    public void RightFacing()
    {
        if (player.GetComponent<PlayerAttack>().facingLeft)
        {
            spear.transform.localPosition = new Vector3(-0.9f, 0, 0);
            player.GetComponent<PlayerAttack>().facingLeft = false;
        }
    }

    void ThrowSpear()
    {
        //throw the spear to the position movePos
        playerX = player.transform.position.x;

        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            targetX = transform.position.x - throwDistance;
        }

        else
        {
            targetX = transform.position.x + throwDistance;
        }

        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);

        movePos = new Vector3(nextX, transform.position.y, transform.position.z);
        transform.position = movePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //if the spear/polearm collides with an enemy, make the enemy take 1 damage
            collision.GetComponent<EnemyHealth>().enemyHealth -= 1;
        }
    }

    IEnumerator SpecialCooldown()
    {
        canUseSpecial = false;
        yield return new WaitForSeconds(0.0f);
        canUseSpecial = true;
    }

    IEnumerator SpearPosition()
    {
        //player moves to the spear's position
        player.GetComponent<PlayerAttack>().spearMove = true;
        player.GetComponent<PlayerAttack>().MoveToSpear();
        yield return new WaitForSeconds(0.01f);
        spear.transform.SetParent(player.transform);
        spearThrown = false;
    }

    IEnumerator colliderActive()
    {
        //sets the spear's collider to active when the player attacks
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }
}
