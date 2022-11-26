using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputSystem : MonoBehaviour
{
    // TODO: Change it later
    public float speed;
    public float dashSpeed;
    public Rigidbody rigidBody;
    public PlayerInput playerInput;
    public PlayerInputActions inputActions;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
        inputActions.Player.Dash.performed += Dash_performed;
        inputActions.Player.LightAttack.performed += LightAttack_performed;
        inputActions.Player.HeavyAttack.performed += HeavyAttack_performed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();

        if (inputVector != Vector2.zero)
        {
            rigidBody.MovePosition(transform.position + new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(new Vector3(inputVector.x, 0, inputVector.y));
        }
    }
    private void Dash_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 inputVector = Vector3.forward;
                
            rigidBody.AddForce(inputVector * dashSpeed, ForceMode.Impulse);
        }
    }

    private void LightAttack_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("l-a");
        }
    }
    
    private void HeavyAttack_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("h-a");
        }
    }
}
