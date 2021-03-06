using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    SpriteRenderer sr;
    bool enemyStunned = false;

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawner;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    private void Update()
    {
        if (enemy.GetComponent<EnemyHealth>().enemyHealth <= 0.0f)
        {
           // spawner.GetComponent<EnemyWaveSpawning>().enemyList.Remove(enemy);
            Destroy(enemy);
        }
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
        // Debug.Log("Enemy has been stunned");
        enemy.GetComponent<EnemyPatrol>().enabled = false;
        sr.color = Color.red;
        yield return new WaitForSeconds(1.3f);
        enemyStunned = false;
        enemy.GetComponent<EnemyPatrol>().enabled = true;
        sr.color = Color.white;
    }
}
