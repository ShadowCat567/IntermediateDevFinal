using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //variables related to enemy and enemy health

    public GameObject player;
    public float playerHealth = 1.0f;
    float maxHealth = 1.0f;
    // public TMP_Text enemyHealthTxt;
    [SerializeField] GameObject healthBar;

    // Update is called once per frame
    void Update()
    {
        //updates EnemyHealthTxt to enemy's current health
        healthBar.GetComponent<PlayerHealthBar>().UpdateHealthBar(playerHealth);
        //   enemyHealthTxt.text = "Health: " + enemyHealth;
    }
}
