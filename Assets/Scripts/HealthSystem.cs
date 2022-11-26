using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health values")]
    public int maxHealth = 10;
    public float curentHealth = 10;
    
    public Color healthColor;
    public List<Image> hearths;

    public void TakeDamage()
    {
        curentHealth--;

        if (curentHealth <= 0)
            Die();

        RemoveHealthFromBar();
    }

    public void RemoveHealthFromBar()
    {
        if (curentHealth / 2 == 4.5f)
            hearths[4].fillAmount = 0.5f;
        if (curentHealth / 2 == 4.0f)
            hearths[4].fillAmount = 0.0f;
        if (curentHealth / 2 == 3.5f)
            hearths[3].fillAmount = 0.5f;
        if (curentHealth / 2 == 3.0f)
            hearths[3].fillAmount = 0.0f;
        if (curentHealth / 2 == 2.5f)
            hearths[2].fillAmount = 0.5f;
        if (curentHealth / 2 == 2.0f)
            hearths[2].fillAmount = 0.0f;
        if (curentHealth / 2 == 1.5f)
            hearths[1].fillAmount = 0.5f;
        if (curentHealth / 2 == 1.0f)
            hearths[1].fillAmount = 0.0f;
        if (curentHealth / 2 == 0.5f)
            hearths[0].fillAmount = 0.5f;
    }

    //public void 

    public void Die()
    {
        // TODO: Checkpoints
    }

    private void Start()
    {
        curentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
    }
}
