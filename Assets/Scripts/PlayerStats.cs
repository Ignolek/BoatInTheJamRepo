using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    static public PlayerStats Instance;

    public float lightAttackDamage;
    public float heavyAttackDamage;


    void Start()
    {
        if (Instance == null)
            Instance = this;
    }
}
