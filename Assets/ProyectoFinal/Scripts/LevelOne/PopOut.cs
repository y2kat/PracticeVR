using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOut : MonoBehaviour
{
    public GameObject objectToDeactivate;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToDeactivate.SetActive(false);
        }
    }
}
