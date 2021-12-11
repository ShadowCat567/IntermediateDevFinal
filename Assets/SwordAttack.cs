using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public Transform playerTransform;
    public GameObject player;
    public GameObject sword;

    public float cooldownTimerSword = 1.0f;
    public float swordCounter;
    public TMP_Text swordCooldownTxt;

    [SerializeField] GameObject ProjectilePrefab;

    bool canUseSpecial = true;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerAttack>().swordActive)
        {
            //when left mouse button is pressed, attack
            StartCoroutine(colliderActive());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F) && canUseSpecial && player.GetComponent<PlayerAttack>().swordActive)
        {
            //perform special ability
            WaveAttack();
            StartCoroutine(SpecialCooldown(cooldownTimerSword, swordCounter));
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
        if (collision.gameObject.tag == "Enemy" && player.GetComponent<PlayerAttack>().swordActive)
        {
            //if sword collides with an enemy, make the enemy take one damage
            collision.GetComponent<EnemyHealth>().enemyHealth -= 0.1f;
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
          //  Debug.Log("Moving left");
        }
    }

    IEnumerator SpecialCooldown(float cooldown, float counter)
    {
        counter = 0;
        canUseSpecial = false;
        while (counter < cooldown)
        {
            swordCooldownTxt.text = "Sword Cooldown: " + counter;
            yield return new WaitForSeconds(0.1f);
            counter ++;
        }

        yield return new WaitForSeconds(cooldown);
        swordCooldownTxt.text = "Sword Cooldown: READY";
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
