using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrophy : Trophy
{
    private bool inPlace = false; // Variable para almacenar si el trofeo está en su lugar

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
}
