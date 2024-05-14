using UnityEngine;

public class FallZone : MonoBehaviour
{
    public Transform respawnPoint; // Aseg�rate de asignar el punto de respawn en el Inspector de Unity

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que tu jugador tenga la etiqueta "Player"
        {
            other.transform.position = respawnPoint.position;
        }
    }
}
