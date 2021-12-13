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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(float curHealth = 1)
    {
        localScale.x = curHealth;
        transform.localScale = localScale;
    }

}
