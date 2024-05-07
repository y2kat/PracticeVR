using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverLevelOne : MonoBehaviour
{
    public bool isActive = false;
    private AudioSource audioSource;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnActivate);
        grabInteractable.deactivated.AddListener(OnDeactivate);
    }

    void OnActivate(ActivateEventArgs args)
    {
        isActive = true;
        audioSource.Play();
    }

    void OnDeactivate(DeactivateEventArgs args)
    {
        isActive = false;
    }
}
