using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public GameObject messagePanel;         // Panel con el mensaje principal
    public GameObject promptPanel;          // Panel con "Presiona E para interactuar"
    public string messageText = "¡Hola viajero! Ten cuidado en el bosque.";
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear)
        {
            promptPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                messagePanel.SetActive(true);
                messagePanel.GetComponentInChildren<UnityEngine.UI.Text>().text = messageText;
                promptPanel.SetActive(false); // Oculta el prompt al mostrar el mensaje
            }
        }

        if (messagePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            messagePanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            messagePanel.SetActive(false);
            promptPanel.SetActive(false);
        }
    }
}