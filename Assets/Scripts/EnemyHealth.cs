using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    //variables related to enemy and enemy health

    public GameObject enemy;
    public int enemyHealth = 3;
    public TMP_Text enemyHealthTxt;

    // Update is called once per frame
    void Update()
    {
        //updates EnemyHealthTxt to enemy's current health
        enemyHealthTxt.text = "Health: " + enemyHealth;
    }
}
