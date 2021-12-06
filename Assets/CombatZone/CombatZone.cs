using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatZone : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer sr;
    public bool combatZoneStart = false;
    public bool completedCombat = false;
    public bool combatZoneEntered = false;

    [SerializeField] TMP_Text EnemyCombatTxt;
    [SerializeField] GameObject EnemyWaveSpawner;
    public GameObject exitWall;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        exitWall.GetComponent<BoxCollider2D>().isTrigger = true;
        exitWall.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && completedCombat == false)
        {
            combatZoneStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(combatZoneStart)
        {
            combatZoneEntered = true;
            boxCollider.isTrigger = false;
            exitWall.GetComponent<BoxCollider2D>().isTrigger = false;
            exitWall.GetComponent<SpriteRenderer>().color = Color.white;
            sr.color = Color.white;
            //each combat zone needs its own text
            EnemyCombatTxt.text = "Enemies Remaining: " + EnemyWaveSpawner.GetComponent<EnemyWaveSpawning>().enemyList.Count;
        }

        else if(!combatZoneStart)
        {
            boxCollider.isTrigger = true;
            exitWall.GetComponent<BoxCollider2D>().isTrigger = true;
            sr.color = new Color(1, 1, 1, 0.5f);
            exitWall.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            EnemyCombatTxt.text = "";
        }
    }
}
