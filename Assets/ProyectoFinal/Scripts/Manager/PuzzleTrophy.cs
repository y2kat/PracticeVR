using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrophy : Trophy
{
    private bool inPlace = false; // Variable para almacenar si el trofeo está en su lugar
    public TrophyManager trophyManager;

    // Este método se llamará cuando el trofeo se coloque en su socket
    public void PlaceInSocket()
    {
        inPlace = true;
    }

    // Este método se utilizará para comprobar si el trofeo está en su lugar
    public bool IsInPlace()
    {
        return inPlace;
    }

    public bool CanBePickedUp()
    {
        // El trofeo sólo puede ser recogido si es el siguiente en la lista del PuzzleManager
        return puzzleManager.CanPickUpTrophy(this);
    }
}
