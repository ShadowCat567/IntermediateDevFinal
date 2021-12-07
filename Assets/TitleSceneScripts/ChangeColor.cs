using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    SpriteRenderer sr;

    Color exitHover = new Color(0.96f, 0.0f, 0.0f);
    Color exitClick = new Color(0.49f, 0.01f, 0.01f);
    Color exitBase = new Color(0.66f, 0.0f, 0.0f);

    Color startHover = new Color(0.11f, 0.99f, 0.09f);
    Color startClick = new Color(0.08f, 0.52f, 0.07f);
    Color startBase = new Color(0.16f, 0.75f, 0.15f);

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void OnExitHover()
    {
        sr.color = exitHover;
    }

    public void OnExitClick()
    {
        sr.color = exitClick;
    }

    public void ExitRevert()
    {
        sr.color = exitBase;
    }

    public void OnStartHover()
    {
        sr.color = startHover;
    }

    public void OnStartClick()
    {
        sr.color = startClick;
    }

    public void StartRevert()
    {
        sr.color = startBase;
    }
}
