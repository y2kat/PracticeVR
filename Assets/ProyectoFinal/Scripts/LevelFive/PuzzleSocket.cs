using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleSocket : XRSocketInteractor
{
    public PuzzleManager puzzleManager; // Referencia al PuzzleManager

    // ...

    public bool CanAcceptTrophy(PuzzleTrophy trophy)
    {
        // El socket sólo puede aceptar el trofeo si es el siguiente en la lista del PuzzleManager
        return puzzleManager.CanPlaceTrophy(trophy, this);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        PuzzleTrophy puzzleTrophy = interactable.GetComponent<PuzzleTrophy>();
        if (puzzleTrophy != null && !CanAcceptTrophy(puzzleTrophy))
        {
            // Si el interactable es un PuzzleTrophy y este socket no puede aceptarlo, devuelve false
            return false;
        }

        // Si el interactable no es un PuzzleTrophy, o si es un PuzzleTrophy y este socket puede aceptarlo, llama al método base
        return base.CanSelect(interactable);
    }
}
