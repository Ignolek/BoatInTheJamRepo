using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputSystem : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    [Header("Dash")]
    public float dashSpeed;
    public float dashTimeCooldown;
    public float timeBetweenDashes;
    private float cTimeBetweenDashes;
    private bool dashPerformed;
    [Header("Lock in place")]
    public bool lockInPlace;
    public bool lockRotation;
    [Header("Animations")]
    public Animator bodyAnimator;
    public Animator mirrorAnimator;
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerInputActions inputActions;
    [Header("Sounds")]
    public AudioSource steps;
    public AudioSource lightAttack;
    public AudioSource heavyAttack;

    public void ChangePlayerColor()
    {
        var healthSystem = FindObjectOfType<HealthSystem>().currentColor;

        GameObject.Find("Tooth").GetComponent<Renderer>().material.SetColor("_Color", healthSystem);
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
        inputActions.Player.Dash.performed += Dash_performed;
        inputActions.Player.LightAttack.performed += LightAttack_performed;
        inputActions.Player.HeavyAttack.performed += HeavyAttack_performed;
        inputActions.Player.StayInPlace.performed += StayInPlace_performed;
    }

    private void FixedUpdate()
    {
        if (dashPerformed)
        {
            if (cTimeBetweenDashes >= timeBetweenDashes)
            {
                dashPerformed = false;
                cTimeBetweenDashes = 0;
            }

            cTimeBetweenDashes += Time.deltaTime;
        }


        if (lockInPlace && lockRotation)
        {
            bodyAnimator.SetBool("Move", false);
            return;
        }

        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        if (bodyAnimator.GetBool("Move"))
        {
            if(!steps.isPlaying)
                steps.Play();
        }

        if (!lockInPlace)
        {
            rigidBody.MovePosition((transform.position + Quaternion.Euler(0, -133, 0) * new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * speed));
            bodyAnimator.SetBool("Move", true);
        }

        if (inputVector != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0, -133, 0) * new Vector3(inputVector.x, 0, inputVector.y));
        }

        if (inputVector == Vector2.zero)
        {
            bodyAnimator.SetBool("Move", false);
        }
    }

    private void Dash_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (dashPerformed)
            return;

        dashPerformed = true;

        Vector3 inputVector = transform.forward;

        rigidBody.AddForce(inputVector * dashSpeed, ForceMode.VelocityChange);
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashTimeCooldown);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    private void LightAttack_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        mirrorAnimator.SetTrigger("LightAttack");
        lightAttack.Play();
    }
    
    private void HeavyAttack_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        lockInPlace = true;
        lockRotation = true;
        mirrorAnimator.SetTrigger("HeavyAttack");
        heavyAttack.Play();
    }

    private void StayInPlace_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        bodyAnimator.SetBool("Move", false);

        lockInPlace = !lockInPlace;
    }
}
