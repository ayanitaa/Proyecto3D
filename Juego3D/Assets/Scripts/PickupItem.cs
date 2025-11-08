using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int value = 1; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Buscar el script del jugador que maneja las monedas
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.AddCoins(value); 
            }

            Debug.Log("Moneda recogida!");
            Destroy(gameObject);
        }
    }
}