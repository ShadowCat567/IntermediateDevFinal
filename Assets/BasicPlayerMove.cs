using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;

    Camera mainCamera;

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float moveX;

    public GameObject swordObj;
    public GameObject spearObj;
    public GameObject sheildObj;

    float maxDashdistance = 5.0f;
    bool dashing;
    float dashSpeed = 180.0f;

    public bool shieldDash;
    public float shieldDashSpeed = 200.0f;

    enum currentWeapon
    {
        none, sword, spear, shield
    }

    [SerializeField] currentWeapon weaponInHand = currentWeapon.sword;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void PlayerControls()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
        TargetDash();

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            weaponChanger();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
    }

    void TargetDash()
    {
        float distance;
        float enemyDistance;

        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

            if(ray.collider != null && ray.collider.tag == "Enemy")
            {
                enemyDistance = Vector3.Distance(ray.collider.transform.position, player.transform.position);

                if (enemyDistance > maxDashdistance)
                {
                 //   Debug.Log("using max distance");
                    distance = maxDashdistance;
                    dashing = true;
                    ExecuteDash(distance, ray.collider);
                }

                else
                {
                  //  Debug.Log("Using enemy distance");
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
        Vector3 targetPositon;
        Vector3 currentPosition = transform.position;

        if (dashing)
        {
          //  Debug.Log("Dashing is " + dashing.ToString());
          //  Debug.Log("Dash distance: " + distance.ToString());
            transform.position = Vector2.Lerp(currentPosition, enemy.transform.position, dashSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                dashing = false;
            }
        }
    }

    public void SpecialAttack(GameObject enemy)
    {
        //Debug.Log("Distance: " + distance.ToString());
        Vector3 currentPositon = transform.position;
       // Vector3 targetPosition = transform.position + Vector3.right * distance;

        if (shieldDash)
        {
            transform.position = Vector2.Lerp(currentPositon, enemy.transform.position, shieldDashSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, enemy.transform.position) < 0.5f)
            {
                shieldDash = false;
            }
        }
    }

    void weaponChanger()
    {
        switch(weaponInHand)
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
