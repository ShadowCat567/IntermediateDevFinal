using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClicker : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] GameObject ExitButton;
    [SerializeField] GameObject StartButton;

    Color startBase = new Color(0.16f, 0.75f, 0.15f);
    Color exitBase = new Color(0.66f, 0.0f, 0.0f);

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       /// CheckClick();
        HoverCheck();
       // StartButton.GetComponent<SpriteRenderer>().color = startBase;
       // ExitButton.GetComponent<SpriteRenderer>().color = exitBase;
    }

    void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

            if(ray.collider != null && ray.collider.tag == "StartButton")
            {
                ray.collider.GetComponent<ChangeColor>().OnStartClick();
                //change to main game scene
            }

            if(ray.collider != null && ray.collider.tag == "EndButton")
            {
                ray.collider.GetComponent<ChangeColor>().OnExitClick();
                Application.Quit();
            }
        }
    }

    void HoverCheck()
    {
        RaycastHit2D ray = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Input.mousePosition));

        if (ray.collider != null && ray.collider.tag == "StartButton" && Input.GetMouseButtonDown(0))
        {
             ray.collider.GetComponent<ChangeColor>().OnStartClick();
             //change to main game scene
        }

        else if (ray.collider != null && ray.collider.tag == "StartButton" && !Input.GetMouseButtonDown(0))
        {
             ray.collider.GetComponent<ChangeColor>().OnStartHover();
        }

        else
        {
             StartButton.GetComponent<SpriteRenderer>().color = startBase;
        }

        if (ray.collider != null && ray.collider.tag == "EndButton" && Input.GetMouseButtonDown(0))
        {
             ray.collider.GetComponent<ChangeColor>().OnExitClick();
             Application.Quit();
        }

        else if (ray.collider != null && ray.collider.tag == "EndButton" && !Input.GetMouseButtonDown(0))
        {
             ray.collider.GetComponent<ChangeColor>().OnExitHover();
        }

        else
        {
             ExitButton.GetComponent<SpriteRenderer>().color = exitBase;
        }
    }
}
