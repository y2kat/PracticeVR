using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    // Referencia a la rueda y al objeto 3D.
    public GameObject wheel;
    public GameObject object3D;

    void Start()
    {
        // Establece el objeto 3D como hijo de la rueda.
        object3D.transform.parent = wheel.transform;
    }

    // Aquí puedes agregar el código para rotar la rueda, etc.
}

