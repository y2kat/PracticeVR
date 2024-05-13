using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrophyManager : MonoBehaviour
{
    /*public List<XRGrabInteractable> Trophies = new List<XRGrabInteractable>();
    public List<XRSocketInteractor> Sockets = new List<XRSocketInteractor>();

    private int currentTrophyIndex = 0;

    private void Start()
    {
        for (int i = 0; i < Trophies.Count; i++)
        {
            Trophies[i].selectEntered.AddListener(OnTrophyGrabbed);
            Sockets[i].selectEntered.AddListener(OnTrophySocketed);
        }

        // Al inicio, solo el primer trofeo puede ser agarrado
        for (int i = 1; i < Trophies.Count; i++)
        {
            Trophies[i].interactionLayers = 0;
        }
    }

    private void OnTrophyGrabbed(SelectEnterEventArgs args)
    {
        if (args.interactableObject as XRBaseInteractable != Trophies[currentTrophyIndex])
        {
            (args.interactorObject as XRBaseInteractor).Deselect();
        }
    }

    private void OnTrophySocketed(SelectEnterEventArgs args)
    {
        // Permitir que el siguiente trofeo sea agarrado
        currentTrophyIndex++;
        if (currentTrophyIndex < Trophies.Count)
        {
            Trophies[currentTrophyIndex].interactionLayers = LayerMask.GetMask("Default");
        }
    }*/
}
