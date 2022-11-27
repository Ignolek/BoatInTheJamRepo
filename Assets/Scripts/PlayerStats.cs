using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public IEnumerator Rumble(float duration)
    {
        Gamepad.current.SetMotorSpeeds(0f, 1f);
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
