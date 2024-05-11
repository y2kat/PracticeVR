using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    public Inventory inventory;
    public string nextLevelSceneName;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrab);
    }

    void HandleGrab(SelectEnterEventArgs args)
    {
        inventory.AddTrophy();
        SceneManager.LoadScene(nextLevelSceneName);
        Destroy(gameObject);
    }
}

