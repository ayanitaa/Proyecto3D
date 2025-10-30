using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float rotationSpeed = 100f; // Velocidad de rotación
    public float gravity = -9.81f; // Gravedad aplicada al personaje
    public Camera mouseOrbitCamera; // Cámara orbital para referencia

    private CharacterController controller;
    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        //    // Entrada de movimiento (W y S para adelante y atrás)
        //    float vertical = Input.GetAxis("Vertical"); // Movimiento adelante/atrás

        //    // Entrada de rotación (A y D para girar)
        //    float horizontal = Input.GetAxis("Horizontal"); // Rotación izquierda/derecha

        //    // Determinar la dirección según la cámara activa
        //    Vector3 moveDirection = Vector3.zero;

        //    if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeSelf) // Si la cámara orbital está activa
        //    {
        //        // Usar la dirección de la cámara orbital
        //        Vector3 forward = mouseOrbitCamera.transform.forward;
        //        forward.y = 0; // Ignorar la inclinación vertical
        //        forward.Normalize();
        //        moveDirection = forward * vertical;
        //    }
        //    else
        //    {
        //        // Usar la dirección del jugador
        //        moveDirection = transform.forward * vertical;

        //        // Rotar al personaje con A y D
        //        if (horizontal != 0)
        //        {
        //            float rotation = horizontal * rotationSpeed * Time.deltaTime;
        //            transform.Rotate(0, rotation, 0);
        //        }
        //    }

        //    // Mover al personaje
        //    controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        //    // Aplicar gravedad
        //    if (!controller.isGrounded)
        //    {
        //        velocity.y += gravity * Time.deltaTime;
        //        controller.Move(velocity * Time.deltaTime);
        //    }
        //    else
        //    {
        //        velocity.y = 0; // Reiniciar la velocidad vertical si está en el suelo
        //    }


        var keyboard = Keyboard.current; // Nuevo sistema
        if (keyboard == null) return;

        float vertical = 0f;
        float horizontal = 0f;

        // Movimiento adelante/atrás (W/S o flechas)
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
            vertical = 1f;
        else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
            vertical = -1f;

        // Rotación izquierda/derecha (A/D o flechas)
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            horizontal = -1f;
        else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            horizontal = 1f;

        Vector3 moveDirection = Vector3.zero;

        // Si la cámara orbital está activa
        if (mouseOrbitCamera != null && mouseOrbitCamera.gameObject.activeSelf)
        {
            Vector3 forward = mouseOrbitCamera.transform.forward;
            forward.y = 0;
            forward.Normalize();
            moveDirection = forward * vertical;
        }
        else
        {
            moveDirection = transform.forward * vertical;

            if (horizontal != 0)
            {
                float rotation = horizontal * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotation, 0);
            }
        }

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Aplicar gravedad
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            velocity.y = 0;
        }
    }
}


