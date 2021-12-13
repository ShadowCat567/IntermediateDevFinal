using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShieldAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    [SerializeField] Animator shieldBlockAnim;

    SpriteRenderer sr;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Image shieldCooldownImg;

    public GameObject player;
    public GameObject shield;

    bool hittingEnemy;
    bool canUseSpecial = true;
    //public TMP_Text sheildCooldownTxt;
    public float cooldownShield = 1.5f;
    public float shieldCounter;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        boxCollider.isTrigger = true;
        shieldBlockAnim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && player.GetComponent<PlayerAttack>().shieldActive)
        {
            //if the shield is colliding with the enemy, the enemy is stunned
            hittingEnemy = true;
            collision.GetComponent<EnemyBehavior>().EnemyStunned();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && player.GetComponent<PlayerAttack>().shieldActive)
        {
            //when left mouse botton is pressed, the shield blocks
            shieldBlockAnim.enabled = true;
            StartCoroutine(Block());
        }

        if (Input.GetKeyDown(KeyCode.M) && canUseSpecial && player.GetComponent<PlayerAttack>().shieldActive)
        {
            //finds a targetEnemy and moves to that targetEnemy, stunning it
            GameObject targetEnemy = EnemyInDetectDistance();
            if (targetEnemy)
            {
                StartCoroutine(SheildBash());
                player.GetComponent<PlayerAttack>().shieldDash = true;
                player.GetComponent<PlayerAttack>().SpecialAttack(targetEnemy);
            }

            if (shieldCounter == cooldownShield)
            {
                canUseSpecial = true;
            }

            shieldCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
            StartCoroutine(SpecialCooldown(cooldownShield, shieldCounter));
            //perform special ability
            //if enemy is within a certain range, dash to the enemy -- how to get the enemy that is in that range
            //dash distance = distance to enemy, this would probably more flexible...just need to get the detection down
            //really short range dash, shield becomes solid and temporarily stuns emeny (stun means enemy cannot attack or move)
        }
    }

    public void LeftFacing()
    {
        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            shield.transform.localPosition = new Vector3(0.96f, -0.1f, 0);
            sr.sprite = rightSprite;
            player.GetComponent<PlayerAttack>().facingLeft = true;
        }
    }

    public void RightFacing()
    {
        if (player.GetComponent<PlayerAttack>().facingLeft)
        {
            shield.transform.localPosition = new Vector3(-1.1f, 0.1f, 0);
            sr.sprite = leftSprite;
            player.GetComponent<PlayerAttack>().facingLeft = false;
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

    IEnumerator SpecialCooldown(float cooldown, float counter)
    {
        counter = 0;
        float opacity = 0.4f;
        canUseSpecial = false;
        while (counter < cooldown)
        {
            //sheildCooldownTxt.text = "Shield Cooldown: " + counter;
            shieldCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, opacity);
            yield return new WaitForSeconds(0.5f);
            counter += 0.5f;
            opacity += 0.15f;
        }

        yield return new WaitForSeconds(1.5f);
        canUseSpecial = true;
        shieldCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
       // sheildCooldownTxt.text = "Shield Cooldown: READY";
    }

    IEnumerator Block()
    {
        //activates the sheild's collider for 0.5 seconds to block incoming attacks
        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            shieldBlockAnim.Play("ShieldAttkR");
        }

        else
        {
            shieldBlockAnim.Play("ShieldAttkL");
        }

        boxCollider.enabled = true;
        boxCollider.isTrigger = false;
       // sr.color = new Color(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        boxCollider.isTrigger = true;
        shieldBlockAnim.enabled = false;
      //  sr.color = new Color(1.0f, 1.0f, 1.0f);
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
