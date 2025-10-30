using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera thirdPersonCamera; // Cámara en tercera persona
    public Camera mouseOrbitCamera; // Cámara orbital
    public Transform playerBody; // Cuerpo del jugador para sincronizar la posición relativa

    public float orbitSensitivity = 500f; // Sensibilidad de la cámara orbital
    public float orbitDistance = 5f; // Distancia de la cámara orbital
    private float orbitXRotation = 0f; // Rotación vertical de la cámara orbital
    private float orbitYRotation = 0f; // Rotación horizontal de la cámara orbital

    private void Start()
    {
        // Activar inicialmente la cámara en tercera persona
        ActivateCamera(thirdPersonCamera, false);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;

        if (keyboard.tKey.wasPressedThisFrame)
            SmoothSwitchToCamera(thirdPersonCamera);
        else if (keyboard.uKey.wasPressedThisFrame)
            SmoothSwitchToCamera(mouseOrbitCamera);

        if (mouseOrbitCamera.gameObject.activeSelf)
        {
            ControlMouseOrbitCamera(mouse);
        }
    }

    private void ControlMouseOrbitCamera(Mouse mouse)
    {
        if (mouse == null) return;

        float mouseX = mouse.delta.x.ReadValue() * orbitSensitivity * Time.deltaTime;
        float mouseY = mouse.delta.y.ReadValue() * orbitSensitivity * Time.deltaTime;

        orbitYRotation += mouseX;
        orbitXRotation -= mouseY;
        orbitXRotation = Mathf.Clamp(orbitXRotation, -35f, 60f);

        Quaternion rotation = Quaternion.Euler(orbitXRotation, orbitYRotation, 0f);
        Vector3 offset = rotation * new Vector3(0, 1.5f, -orbitDistance);
        mouseOrbitCamera.transform.position = playerBody.position + offset;
        mouseOrbitCamera.transform.LookAt(playerBody);
    }

    private void SmoothSwitchToCamera(Camera targetCamera)
    {
        StartCoroutine(SmoothSwitchCoroutine(targetCamera));
    }

    private IEnumerator SmoothSwitchCoroutine(Camera targetCamera)
    {
        Camera currentCamera = thirdPersonCamera.gameObject.activeSelf ? thirdPersonCamera : mouseOrbitCamera;

        Vector3 startPosition = currentCamera.transform.position;
        Quaternion startRotation = currentCamera.transform.rotation;

        Vector3 targetPosition = targetCamera.transform.position;
        if (targetCamera == mouseOrbitCamera)
        {
            targetPosition.y += 1.0f;
        }

        Quaternion targetRotation = targetCamera.transform.rotation;

        float transitionTime = 0.5f;
        float elapsedTime = 0f;

        targetCamera.gameObject.SetActive(true);

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;

            currentCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            currentCamera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        ActivateCamera(targetCamera, true);
    }

    private void ActivateCamera(Camera targetCamera, bool finalize)
    {
        if (targetCamera == mouseOrbitCamera && finalize)
        {
            Vector3 targetPosition = mouseOrbitCamera.transform.position;
            targetPosition.y += 1.0f;
            mouseOrbitCamera.transform.position = targetPosition;
        }

        thirdPersonCamera.gameObject.SetActive(targetCamera == thirdPersonCamera);
        mouseOrbitCamera.gameObject.SetActive(targetCamera == mouseOrbitCamera);

        Debug.Log($"{targetCamera.name} activada.");
    }

    private void ControlMouseOrbitCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * orbitSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * orbitSensitivity * Time.deltaTime;

        orbitYRotation += mouseX;
        orbitXRotation -= mouseY;
        orbitXRotation = Mathf.Clamp(orbitXRotation, -35f, 60f);

        Quaternion rotation = Quaternion.Euler(orbitXRotation, orbitYRotation, 0f);
        Vector3 offset = rotation * new Vector3(0, 1.5f, -orbitDistance);
        mouseOrbitCamera.transform.position = playerBody.position + offset;
        mouseOrbitCamera.transform.LookAt(playerBody);
    }
}