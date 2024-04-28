using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Sword : MonoBehaviour
{
    private XRGrabInteractable _interactable;

    void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();
        _interactable.selectEntered.AddListener(OnGrab);
        _interactable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Sword grabbed");
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        Debug.Log("Sword released");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ghost"))
        {
            Debug.Log("Ghost hit");
            Destroy(other.gameObject);
            GameManager.instance.enemiesDefeated++;
        }
    }
}


