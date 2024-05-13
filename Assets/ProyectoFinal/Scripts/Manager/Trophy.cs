using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    public Inventory inventory;
    public string nextLevelSceneName;
    private XRGrabInteractable grabInteractable;
    private Vector3 originalScale;

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrab);
        grabInteractable.selectExited.AddListener(HandleRelease); // Agrega el manejador de eventos para cuando el trofeo es soltado.
        originalScale = transform.localScale;
        Debug.Log("Escala original: " + originalScale);
    }

    void HandleGrab(SelectEnterEventArgs args)
    {
        inventory.AddTrophy();

        // Si el jugador ha recogido el último trofeo, haz que el trofeo sea hijo del controlador del jugador.
        if (inventory.trophyCount == inventory.totalTrophies)
        {
            transform.SetParent(args.interactorObject.transform);
            transform.localScale = originalScale * 0.5f;
            Debug.Log("Nueva escala: " + transform.localScale);
        }
        else
        {
            SceneManager.LoadScene(nextLevelSceneName);
            Destroy(gameObject);
        }
    }

    void HandleRelease(SelectExitEventArgs args)
    {
        // Cuando el trofeo es soltado, restablece su escala a la original.
        transform.localScale = originalScale;
    }
}

