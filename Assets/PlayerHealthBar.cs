using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
    }

    public void UpdateHealthBar(float curHealth = 1)
    {
        localScale.x = curHealth;
        transform.localScale = localScale;
    }

}
