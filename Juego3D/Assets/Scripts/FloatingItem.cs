using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 0.25f; // Altura de la flotación
    public float floatFrequency = 1f;    // Velocidad de la flotación
    public float rotationSpeed = 50f;    // Velocidad de rotación

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimiento vertical tipo seno
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);

        // Rotación suave
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}