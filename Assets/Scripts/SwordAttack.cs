using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwordAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public Transform playerTransform;
    public GameObject player;
    public GameObject sword;

    [SerializeField] Animator swordAttkAnim;

    SpriteRenderer sr;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Image swordCooldownImg;

    public float cooldownTimerSword = 1.0f;
    public float swordCounter;
  //  public TMP_Text swordCooldownTxt;

    [SerializeField] GameObject ProjectilePrefab;

    bool canUseSpecial = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        swordAttkAnim.enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerAttack>().swordActive)
        {
            //when left mouse button is pressed, attack
            swordAttkAnim.enabled = true;
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
            swordCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
            StartCoroutine(SpecialCooldown(cooldownTimerSword, swordCounter));
        }
    }

    public void LeftFacing()
    {
        if(!player.GetComponent<PlayerAttack>().facingLeft)
        {
            sword.transform.localPosition = new Vector3(0.87f, -0.34f, 0);
            sr.sprite = rightSprite;
            player.GetComponent<PlayerAttack>().facingLeft = true;
        }
    }

    public void RightFacing()
    {
        if(player.GetComponent<PlayerAttack>().facingLeft)
        {
            sword.transform.localPosition = new Vector3(-0.87f, -0.34f, 0);
            sr.sprite = leftSprite;
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
        projectile = Instantiate(ProjectilePrefab, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
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
        float opacity = 0.4f;
        canUseSpecial = false;
        while (counter < cooldown)
        {
           // swordCooldownTxt.text = "Sword Cooldown: " + counter;
            swordCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, opacity);
            yield return new WaitForSeconds(0.1f);
            counter ++;
            opacity += 0.2f;
        }

        yield return new WaitForSeconds(cooldown);
        swordCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
       // swordCooldownTxt.text = "Sword Cooldown: READY";
        canUseSpecial = true;
    }

    IEnumerator KillProjectile(float lifetime, GameObject projectile)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(projectile);
    }

    IEnumerator colliderActive()
    {
        if(!player.GetComponent<PlayerAttack>().facingLeft)
        {
            swordAttkAnim.Play("SwordAttkL");
        }

        else
        {
            swordAttkAnim.Play("SwordAttkR");
        }

        //activate the collider for 0.8 seconds to complete the attack
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.6f);
        boxCollider.enabled = false;
        swordAttkAnim.enabled = false;
    }
}
