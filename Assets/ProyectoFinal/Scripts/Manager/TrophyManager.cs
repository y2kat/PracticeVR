using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class TrophyManager : MonoBehaviour
{
    public List<PuzzleTrophy> trophies; // Lista de trofeos
    public List<PuzzleSocket> sockets; // Lista de sockets
    private int currentTrophyIndex = 0; // Índice del trofeo actual

    void Update()
    {
        // Comprueba si todos los trofeos están en sus respectivos sockets
        if (AllTrophiesInPlace())
        {
            // Cambia a la siguiente escena
            SceneManager.LoadScene("NextScene");
        }
    }

    public bool CanPickUpTrophy(PuzzleTrophy trophy)
    {
        // El jugador sólo puede recoger el trofeo si es el siguiente en la lista
        return trophy == trophies[currentTrophyIndex];
    }

    public bool CanPlaceTrophy(PuzzleTrophy trophy, Socket socket)
    {
        // El trofeo sólo puede ser colocado en el socket correspondiente
        return trophy == trophies[currentTrophyIndex] && socket == sockets[currentTrophyIndex];
    }

    public void TrophyPlaced()
    {
        // Incrementa el índice del trofeo actual
        currentTrophyIndex++;
    }

    private bool AllTrophiesInPlace()
    {
        // Comprueba si todos los trofeos están en su lugar
        for (int i = 0; i < trophies.Count; i++)
        {
            if (!trophies[i].IsInPlace())
            {
                return false;
            }
        }
        return true;
    }
}






