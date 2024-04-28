using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lightsaber : MonoBehaviour
{
    public GameObject saber;
    bool turnOn;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable _interactable = GetComponent<XRGrabInteractable>();
        _interactable.activated.AddListener(turnOnSaber);


    }

    public void turnOnSaber(ActivateEventArgs arg)
    {
        turnOn = ! turnOn;
        saber.SetActive(turnOn);


    }

}