using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public GameObject[] wheels;
    public GameObject[] images;
    public float[] correctRotations; // Los valores de rotación correctos para cada objeto.

    public GameObject objectToActivate;

    void Start()
    {
        // Al inicio, solo la primera imagen y la primera rueda son visibles.
        for (int i = 1; i < images.Length; i++)
        {
            images[i].SetActive(false);
            wheels[i].SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            Debug.Log(wheels[i].name + " Rotación: " + wheels[i].transform.localEulerAngles.y);

            // Comprueba si la rueda está alineada correctamente.
            if (IsWheelAligned(wheels[i], correctRotations[i]))
            {
                Debug.Log("correkto :V");
                // Si la rueda está alineada y no es la última, haz que la siguiente imagen y rueda sean visibles.
                if (i < wheels.Length - 1)
                {
                    images[i + 1].SetActive(true);
                    wheels[i + 1].SetActive(true);
                }
                // Si la rueda alineada es la última, activa y desactiva los objetos correspondientes.
                else if (i == wheels.Length - 1)
                {
                    objectToActivate.SetActive(true);
                }
            }
        }
    }

    bool IsWheelAligned(GameObject wheel, float correctRotation)
    {
        // Comprueba si la rotación en el eje Y de la rueda es igual a la rotación correcta.
        float wheelRotation = wheel.transform.localEulerAngles.y;
        return Mathf.Abs(wheelRotation - correctRotation) < 0.1f; // Permite un pequeño margen de error.
    }
}
