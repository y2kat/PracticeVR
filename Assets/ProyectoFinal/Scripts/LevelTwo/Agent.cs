using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public string id; // El ID �nico de este agente

    public void OnShot()
    {
        // Comprobar si este agente es el siguiente en la secuencia
        if (PuzzleController.instance.IsNextInSequence(this.id))
        {
            // Si es as�, el jugador ha acertado
            PuzzleController.instance.RegisterHit(this);
        }
        else
        {
            // Si no, el jugador ha fallado
            PuzzleController.instance.RegisterMiss(this);
        }
    }
}

