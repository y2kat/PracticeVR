using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Teleport : MonoBehaviour {
    public Transform teleportTarget;
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = teleportTarget.position;
        }
    }
}

