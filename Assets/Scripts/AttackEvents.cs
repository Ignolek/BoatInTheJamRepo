using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvents : MonoBehaviour
{
    public void FinishHeavyAttack()
    {
        transform.parent.transform.parent.GetComponent<MovementInputSystem>().lockInPlace = false;
        transform.parent.transform.parent.GetComponent<MovementInputSystem>().lockRotation = false;
    }
}
