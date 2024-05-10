using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Trophy : MonoBehaviour
{
    public Inventory inventory;
    public RoomManager roomManager;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        roomManager = GameObject.FindObjectOfType<RoomManager>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HandleGrab);
    }

    void HandleGrab(SelectEnterEventArgs args)
    {
        inventory.AddTrophy();
        roomManager.UnlockNextRoom();
        Destroy(gameObject);
    }
}

