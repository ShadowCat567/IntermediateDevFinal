using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolearmAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public GameObject player;
    public GameObject spear;

    float speed = 1000.0f;

    bool spearThrown = false;
    float throwDistance = 20.0f;
    Vector3 movePos;
    float playerX;
    float targetX;
    float nextX;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(colliderActive());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(spearThrown)
            {
                StartCoroutine(SpearPosition());
            }

            else
            {
                ThrowSpear();
                spear.transform.SetParent(null);
                spearThrown = true;
            }
            //perform special ability
        }
    }

    void ThrowSpear()
    {
        playerX = player.transform.position.x;
        targetX = transform.position.x + throwDistance;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);

        movePos = new Vector3(nextX, transform.position.y, transform.position.z);
        transform.position = movePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().enemyHealth -= 1;
        }
    }

    IEnumerator SpearPosition()
    {
        player.GetComponent<BasicPlayerMove>().spearMove = true;
        player.GetComponent<BasicPlayerMove>().MoveToSpear();
        yield return new WaitForSeconds(0.5f);
        spear.transform.SetParent(player.transform);
        spearThrown = false;
    }

    IEnumerator colliderActive()
    {
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }
}
