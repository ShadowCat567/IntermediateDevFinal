using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMove : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float moveX;

    public GameObject swordObj;
    public GameObject spearObj;
    public GameObject sheildObj;

    enum currentWeapon
    {
        none, sword, spear, shield
    }

    [SerializeField] currentWeapon weaponInHand = currentWeapon.sword;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void PlayerControls()
    {
        moveX = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();

        if(Input.GetKeyDown(KeyCode.E))
        {
            weaponChanger();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
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
