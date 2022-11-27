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
    public Color currentColor;
    public Color[] healthColor;
    public List<Image> hearths;

    [Header("Checkpoints")]
    public Vector3 lastCheckpoint;

    public void RestoreFullHealth()
    {
        curentHealth = maxHealth;

        for (int i = 0; i < 5; i++)
        {
            hearths[i].color = healthColor[0];
            hearths[i].fillAmount = 1;
        }

        currentColor = healthColor[0];

        GameObject.Find("Player").GetComponent<MovementInputSystem>().ChangePlayerColor();
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
        {
            hearths[4].fillAmount = 0.5f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[1];

            currentColor = healthColor[1];
        }
        if (curentHealth / 2 == 4.0f)
        {
            hearths[4].fillAmount = 0.0f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[2];

            currentColor = healthColor[2];
        }
        if (curentHealth / 2 == 3.5f)
        {
            hearths[3].fillAmount = 0.5f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[3];

            currentColor = healthColor[3];
        }
        if (curentHealth / 2 == 3.0f)
        {
            hearths[3].fillAmount = 0.0f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[4];

            currentColor = healthColor[4];
        }
        if (curentHealth / 2 == 2.5f)
        {
            hearths[2].fillAmount = 0.5f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[5];

            currentColor = healthColor[5];
        }
        if (curentHealth / 2 == 2.0f)
        {
            hearths[2].fillAmount = 0.0f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[6];

            currentColor = healthColor[6];
        }
        if (curentHealth / 2 == 1.5f)
        {
            hearths[1].fillAmount = 0.5f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[7];

            currentColor = healthColor[7];
        }
        if (curentHealth / 2 == 1.0f)
        {
            hearths[1].fillAmount = 0.0f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[8];

            currentColor = healthColor[8];
        }
        if (curentHealth / 2 == 0.5f)
        {
            hearths[0].fillAmount = 0.5f;
            for (int i = 0; i < 5; i++)
                hearths[i].color = healthColor[9];

            currentColor = healthColor[9];
        }

        GameObject.Find("Player").GetComponent<MovementInputSystem>().ChangePlayerColor();
    }

    public void Die()
    {
        Debug.Log("ded");
    }

    private void Awake()
    {
        curentHealth = maxHealth;
        currentColor = healthColor[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
    }
}
