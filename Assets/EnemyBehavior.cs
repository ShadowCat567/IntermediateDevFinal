using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    SpriteRenderer sr;
    bool enemyStunned = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    public void EnemyStunned()
    {
        //stun the enemy
        StartCoroutine(StunEnemy());
    }

    IEnumerator StunEnemy()
    {
        //stun the enemy for 0.9 seconds
        enemyStunned = true;
        Debug.Log("Enemy has been stunned");
        sr.color = Color.red;
        yield return new WaitForSeconds(0.9f);
        enemyStunned = false;
        sr.color = Color.white;
    }
}
