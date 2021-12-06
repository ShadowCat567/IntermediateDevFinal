using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatZone : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer sr;
    public bool combatZoneStart = false;
    public bool completedCombat = false;
    public bool combatZoneEntered = false;

    Camera mainCamera;
    public GameObject LockObject;
    public GameObject exitWall;

    private void Awake()
    {
        mainCamera = Camera.main;
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
            mainCamera.transform.position = LockObject.transform.position;
            mainCamera.GetComponent<BasicCameraFollowIgnore>().enabled = false;
           // StartCoroutine(CombatZoneEnd());
        }

        else if(!combatZoneStart)
        {
            boxCollider.isTrigger = true;
            exitWall.GetComponent<BoxCollider2D>().isTrigger = true;
            sr.color = new Color(1, 1, 1, 0.5f);
            exitWall.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            mainCamera.GetComponent<BasicCameraFollowIgnore>().enabled = true;
        }
    }

    /*
    IEnumerator CombatZoneEnd()
    {
        yield return new WaitForSeconds(3.0f);
        completedCombat = true;
        combatZoneStart = false;
    }
    */
}
