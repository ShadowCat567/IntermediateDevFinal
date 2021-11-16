using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer sr;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine(Block());
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //perform special ability
        }
    }

    IEnumerator Block()
    {
        boxCollider.isTrigger = false;
        sr.color = new Color(0.0f, 0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        boxCollider.isTrigger = true;
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }
}
