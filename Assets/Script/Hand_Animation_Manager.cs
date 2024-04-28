using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand_Animation_Manager : MonoBehaviour
{

    public InputActionProperty dedos_ActionAnimator;
    public InputActionProperty puño_ActionAnimator;
    public Animator HandAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = dedos_ActionAnimator.action.ReadValue<float>();
        HandAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = puño_ActionAnimator.action.ReadValue<float>();
        HandAnimator.SetFloat("Grip", gripValue);
    }
}
