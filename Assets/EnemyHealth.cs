using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemy;
    public int enemyHealth = 3;
    public TMP_Text enemyHealthTxt;

    // Update is called once per frame
    void Update()
    {
        enemyHealthTxt.text = "Health: " + enemyHealth;
    }
}
