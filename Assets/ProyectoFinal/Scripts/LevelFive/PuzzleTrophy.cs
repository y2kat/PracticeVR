using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrophy : Trophy
{
    private bool inPlace = false; // Variable para almacenar si el trofeo est� en su lugar

    // Este m�todo se llamar� cuando el trofeo se coloque en su socket
    public void PlaceInSocket()
    {
        inPlace = true;
    }

    // Este m�todo se utilizar� para comprobar si el trofeo est� en su lugar
    public bool IsInPlace()
    {
        return inPlace;
    }
}
