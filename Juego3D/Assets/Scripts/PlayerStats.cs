using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int totalCoins = 0;

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Monedas: " + totalCoins);
        // Aquí puedes actualizar la UI si tienes una
    }
}