using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health values")]
    public int maxHealth = 10;
    public float curentHealth = 10;
    
    [Header("GFX")]
    public Color healthColor;
    public List<Image> hearths;

    [Header("Checkpoints")]
    public Vector3 lastCheckpoint;

    public void RestoreFullHealth()
    {
        curentHealth = maxHealth;

        hearths[0].fillAmount = 1;
        hearths[1].fillAmount = 1;
        hearths[2].fillAmount = 1;
        hearths[3].fillAmount = 1;
        hearths[4].fillAmount = 1;
    }

    public void TakeDamage()
    {
        curentHealth--;

        if (curentHealth <= 0)
            Die();

        RemoveHealthFromBar();
    }

    public bool IsStillAlive()
    {
        if (curentHealth > 0.0f)
            return true;

        return false;
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

    public void Die()
    {
        Debug.Log("ded");
    }

    private void Awake()
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
