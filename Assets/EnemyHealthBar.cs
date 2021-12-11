using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // [SerializeField] Slider healthSlider;
    // Vector3 offset;
    Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
       // healthSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }

    public void UpdateHealthBar(float curHealth = 1)
    {
        localScale.x = curHealth;
        transform.localScale = localScale;
    }

    /*public void Setup(int curhealth, int maxHealth)
    {
        healthSlider.value = curhealth;
        healthSlider.maxValue = maxHealth;
    }
    */
}
