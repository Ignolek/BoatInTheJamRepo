using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        if (GetComponent<BoxCollider>().enabled)
        {
            other.GetComponent<EnemyStateMachine>().TakeDamage(PlayerStats.Instance.lightAttackDamage);
            //other.GetComponent<Boss>().TakeDamage(PlayerStats.Instance.lightAttackDamage);
            CameraMovement cameraShake = GameObject.Find("Camera").GetComponent<CameraMovement>();
            StartCoroutine(cameraShake.Shake(.05f, .2f));
            StartCoroutine(PlayerStats.Instance.Rumble(0.05f));
        }
        if (GetComponent<SphereCollider>().enabled)
        {
            other.GetComponent<EnemyStateMachine>().TakeDamage(PlayerStats.Instance.heavyAttackDamage);
            //other.GetComponent<Boss>().TakeDamage(PlayerStats.Instance.heavyAttackDamage);
        }
    }

    public void HeavyAttackCameraShake()
    {
        CameraMovement cameraShake = GameObject.Find("Camera").GetComponent<CameraMovement>();
        StartCoroutine(cameraShake.Shake(.15f, .4f));
        StartCoroutine(PlayerStats.Instance.Rumble(0.2f));
    }
}
