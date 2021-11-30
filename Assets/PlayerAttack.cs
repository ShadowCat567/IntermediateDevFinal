using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;

    public bool facingLeft;

    Camera mainCamera;

    public GameObject swordObj;
    public GameObject spearObj;
    public GameObject sheildObj;
    public GameObject spearCore;

    //dash related variables
    float maxDashdistance = 5.0f;
    bool dashing;
    float dashSpeed = 180.0f;

    //shield bash related variables
    public bool shieldDash;
    public float shieldDashSpeed = 200.0f;

    //spear special related variables
    public bool spearMove;
    public float spearMoveSpeed = 500.0f;

    //enum for current weapon in hand
    public enum currentWeapon
    {
        none, sword, spear, shield
    }

    public currentWeapon weaponInHand = currentWeapon.sword;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        TargetDash();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //changes the weapon in the player's hand
            weaponChanger();
        }

        Vector3 velocity = transform.position * rb.velocity;

        if (Input.GetKey(KeyCode.A))
        {
            //moving left
            //I know this looks weird, but apperantly I need to have the next weapon in the enum for it to work
            if (weaponInHand == currentWeapon.spear)
            {
                swordObj.GetComponent<SwordAttack>().RightFacing();
            }

            else if (weaponInHand == currentWeapon.shield)
            {
                spearObj.GetComponent<PolearmAttack>().RightFacing();
            }

            else if (weaponInHand == currentWeapon.none)
            {
                sheildObj.GetComponent<ShieldAttack>().RightFacing();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //moving right
            //I know this looks weird, but apperantly I need to have the next weapon in the enum for it to work
            if (weaponInHand == currentWeapon.spear)
            {
                swordObj.GetComponent<SwordAttack>().LeftFacing();
            }

            else if (weaponInHand == currentWeapon.shield)
            {
                spearObj.GetComponent<PolearmAttack>().FacingLeft();
            }

            else if (weaponInHand == currentWeapon.none)
            {
                sheildObj.GetComponent<ShieldAttack>().LeftFacing();
            }
        }
    }

    void TargetDash()
    {
        //distance related local variables
        float distance;
        float enemyDistance;

        if (Input.GetMouseButtonDown(1))
        {
            //combat dash executes when you hit the right mouse button over an enemy
            RaycastHit2D ray = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

            if (ray.collider != null && ray.collider.tag == "Enemy")
            {
                enemyDistance = Vector3.Distance(ray.collider.transform.position, player.transform.position);

                if (enemyDistance > maxDashdistance)
                {
                    //if enemy distance is greater than maxDashDistance, use maxDashDistance to execute dash
                    distance = maxDashdistance;
                    dashing = true;
                    ExecuteDash(distance, ray.collider);
                }

                else
                {
                    //if enemy distance is less than maxDashDistance, use enemy distance to execute dash
                    distance = enemyDistance;
                    dashing = true;
                    ExecuteDash(distance, ray.collider);
                    ray.collider.GetComponent<EnemyHealth>().enemyHealth -= 1;
                }
            }
        }
    }

    void ExecuteDash(float distance, Collider2D enemy)
    {
        //Executes the combat dash
        Vector3 currentPosition = transform.position;

        if (dashing)
        {
            //moves from the current position to the enemy position
            transform.position = Vector2.Lerp(currentPosition, enemy.transform.position, dashSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                //if you get close to enemy's position, dashing = false
                dashing = false;
            }
        }
    }

    public void SpecialAttack(GameObject enemy)
    {
        //Execute the sheild's special attack (shield bash)
        Vector3 currentPositon = transform.position;

        if (shieldDash)
        {
            //move from current position to enemy position
            transform.position = Vector2.Lerp(currentPositon, enemy.transform.position, shieldDashSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                shieldDash = false;
            }
        }
    }

    public void MoveToSpear()
    {
        //Execute the spear/polearm's special attack
        Vector3 currentpos = transform.position;

        if (spearMove)
        {
            //move from current position to spear core's position
            transform.position = Vector2.Lerp(currentpos, spearCore.transform.position, spearMoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, spearCore.transform.position) < 0.5f)
            {
                spearMove = false;
            }
        }
    }

    void weaponChanger()
    {
        //switch which weapon is active 
        switch (weaponInHand)
        {
            case currentWeapon.none:
                swordObj.SetActive(false);
                spearObj.SetActive(false);
                sheildObj.SetActive(false);
                weaponInHand = currentWeapon.sword;
                break;

            case currentWeapon.sword:
                swordObj.SetActive(true);
                spearObj.SetActive(false);
                sheildObj.SetActive(false);
                weaponInHand = currentWeapon.spear;
                break;

            case currentWeapon.spear:
                swordObj.SetActive(false);
                spearObj.SetActive(true);
                sheildObj.SetActive(false);
                weaponInHand = currentWeapon.shield;
                break;

            case currentWeapon.shield:
                swordObj.SetActive(false);
                spearObj.SetActive(false);
                sheildObj.SetActive(true);
                weaponInHand = currentWeapon.none;
                break;
        }
    }
}
