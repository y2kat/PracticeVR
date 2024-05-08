using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lever : MonoBehaviour
{
    public bool isActive = false;
    private AudioSource audioSource;
    private XRBaseInteractable baseInteractable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        baseInteractable = GetComponent<XRBaseInteractable>();
        baseInteractable.selectEntered.AddListener(OnActivate);
        baseInteractable.selectExited.AddListener(OnDeactivate);
    }

    void OnActivate(SelectEnterEventArgs args)
    {
        isActive = true;
        audioSource.Play();
    }

    void OnDeactivate(SelectExitEventArgs args)
    {
        isActive = false;
    }
}
