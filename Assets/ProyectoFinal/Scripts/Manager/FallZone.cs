using UnityEngine;

public class FallZone : MonoBehaviour
{
    public Transform respawnPoint; // Asegúrate de asignar el punto de respawn en el Inspector de Unity

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que tu jugador tenga la etiqueta "Player"
        {
            other.transform.position = respawnPoint.position;
        }
    }
}
