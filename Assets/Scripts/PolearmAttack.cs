using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PolearmAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public GameObject player;
    public GameObject spear;
    public GameObject spearCore;

    [SerializeField] Animator spearAttackAnim;

    SpriteRenderer sr;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] Image spearCooldownImg;

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

    public float cooldownSpear = 4.0f;
    public float spearCounter;
    //public TMP_Text spearCooldownTxt;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        spearAttackAnim.enabled = false;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && player.GetComponent<PlayerAttack>().spearActive)
        {
            //when left mouse button is pressed, attack
            spearAttackAnim.enabled = true;
            StartCoroutine(colliderActive());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.F) && canUseSpecial && player.GetComponent<PlayerAttack>().spearActive)
        {
            if(spearThrown)
            {
                //when F key is pressed and spearThrown is true, player moves to spear
                StartCoroutine(SpearPosition());

                if(spearCounter == cooldownSpear)
                {
                    canUseSpecial = true;
                }

                spearCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
                StartCoroutine(SpecialCooldown(cooldownSpear, spearCounter));
            }

            else
            {
                //when F key is pressed and spearThrown is false, spear is thrown and spearThrown is set to true
                ThrowSpear();
                spear.transform.SetParent(null);
                boxCollider.isTrigger = false;
                spearThrown = true;
            }
        }
    }

    public void FacingLeft()
    {
        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            spear.transform.localPosition = new Vector3(0.9f, -0.02f, 0);
            sr.sprite = rightSprite;
            player.GetComponent<PlayerAttack>().facingLeft = true;
        }
    }

    public void RightFacing()
    {
        if (player.GetComponent<PlayerAttack>().facingLeft)
        {
            spear.transform.localPosition = new Vector3(-0.9f, -0.02f, 0);
            sr.sprite = leftSprite;
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
        if (collision.gameObject.tag == "Enemy" && player.GetComponent<PlayerAttack>().spearActive)
        {
            //if the spear/polearm collides with an enemy, make the enemy take 1 damage
            collision.GetComponent<EnemyHealth>().enemyHealth -= 0.1f;
        }
    }

    IEnumerator SpecialCooldown(float cooldown, float counter)
    {
        counter = 0;
        float opacity = 0.3f;
        canUseSpecial = false;
        while (counter < cooldown)
        {
            spearCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, opacity);
          //  spearCooldownTxt.text = "Spear Cooldown: " + counter;
            yield return new WaitForSeconds(1.0f);
            counter++;
            opacity += 0.15f;
        }

        yield return new WaitForSeconds(cooldown);
        spearCooldownImg.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        canUseSpecial = true;
      //  spearCooldownTxt.text = "Spear Cooldown: READY";
    }

    IEnumerator SpearPosition()
    {
        //player moves to the spear's position
        player.GetComponent<PlayerAttack>().spearMove = true;
        player.GetComponent<PlayerAttack>().MoveToSpear();
        boxCollider.isTrigger = true;
        yield return new WaitForSeconds(0.01f);
        spear.transform.SetParent(player.transform);
        spearThrown = false;
    }

    IEnumerator colliderActive()
    {
        //sets the spear's collider to active when the player attacks
        if (!player.GetComponent<PlayerAttack>().facingLeft)
        {
            spearAttackAnim.Play("SpearAttkL");
        }

        else
        {
            spearAttackAnim.Play("SpearAttkR");
        }

        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.6f);
        boxCollider.enabled = false;
        spearAttackAnim.enabled = false;
    }
}
