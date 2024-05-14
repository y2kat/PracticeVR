using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class SocketManager : MonoBehaviour
{
    public List<XRSocketInteractor> sockets = new List<XRSocketInteractor>();
    public string nextLevelSceneName;

    void Update()
    {
        CheckAllSocketsFilled();
    }

    void CheckAllSocketsFilled()
    {
        foreach (XRSocketInteractor socket in sockets)
        {
            // Si alguno de los sockets no tiene un trofeo, salimos del método.
            if (socket.interactablesSelected.Count == 0)
            {
                return;
            }
        }

        // Si todos los sockets tienen un trofeo, cargamos la siguiente escena.
        SceneManager.LoadScene(nextLevelSceneName);
    }
}
