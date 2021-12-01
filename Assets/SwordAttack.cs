using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public Transform playerTransform;
    public GameObject player;
    public GameObject sword;

    [SerializeField] GameObject ProjectilePrefab;

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
        GameObject projectile;
        projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        StartCoroutine(KillProjectile(0.7f, projectile));
    }

    void ProjectileDecideDirection(GameObject projectile)
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            projectile.GetComponent<WaveProjectile>().MovingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            projectile.GetComponent<WaveProjectile>().MovingLeft = true;
            //Debug.Log("Moving left");
        }
    }

    IEnumerator SpecialCooldown()
    {
        canUseSpecial = false;
        yield return new WaitForSeconds(1.0f);
        canUseSpecial = true;
    }

    IEnumerator KillProjectile(float lifetime, GameObject projectile)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(projectile);
    }

    IEnumerator colliderActive()
    {
        //activate the collider for 0.8 seconds to complete the attack
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.8f);
        boxCollider.enabled = false;
    }
}
