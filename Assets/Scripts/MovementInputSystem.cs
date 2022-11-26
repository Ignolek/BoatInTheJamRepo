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
    [Header("Lock in place")]
    public bool lockInPlace;
    public bool lockRotation;
    [Header("Animations")]
    public Animator bodyAnimator;
    public Animator mirrorAnimator;
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerInputActions inputActions;

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
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();
        
        if (!lockInPlace)
        {
            rigidBody.MovePosition((transform.position + new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * speed));
            bodyAnimator.SetBool("Move", true);
        }
        else
        {
            bodyAnimator.SetBool("Move", false);
        }

        if (inputVector != Vector2.zero && !lockRotation)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(inputVector.x, 0, inputVector.y));
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
    }
    
    private void HeavyAttack_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        lockInPlace = true;
        lockRotation = true;
        mirrorAnimator.SetTrigger("HeavyAttack");
    }

    private void StayInPlace_performed(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        lockInPlace = !lockInPlace;
    }
}
