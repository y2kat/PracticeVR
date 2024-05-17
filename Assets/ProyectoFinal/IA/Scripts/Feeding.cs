using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feeding : MonoBehaviour
{
    PetAgent petAgent;

    void Start()
    {
        petAgent = GameObject.Find("Cat").GetComponent<PetAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            petAgent.feed(gameObject.transform);
        }
    }
}
