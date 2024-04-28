using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayController : MonoBehaviour
{
    [SerializeField] GameObject leftTeleportation, rightTeleportation;

    [SerializeField] InputActionProperty leftActivate, rightActivate;

    [SerializeField] InputActionProperty leftCancel, rightCancel;
    
    void Update()
    {
        leftTeleportation.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1);    
        rightTeleportation.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1);    
    }
}
