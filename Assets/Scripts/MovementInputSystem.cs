using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputSystem : MonoBehaviour
{
    // TODO: Change it later
    public float speed;
    public Rigidbody sphereRigidbody;
    public PlayerInput playerInput;
    public PlayerInputActions inputActions;

    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = inputActions.Player.Movement.ReadValue<Vector2>();
        sphereRigidbody.MovePosition(transform.position + new Vector3(inputVector.x, 0, inputVector.y) * Time.deltaTime * speed);
    }
}
