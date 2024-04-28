using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Cannons : MonoBehaviour
{

    [Header("Cañones")]
    [SerializeField]
    private float fuerza = 10;
    public GameObject balaPrefab;
    public Rigidbody bala;
    public Transform balaTransform;
    XRSocketInteractor sX;


    // Start is called before the first frame update
    void Start()
    {
        sX = GetComponent<XRSocketInteractor>();
        bala = balaPrefab.GetComponent<Rigidbody>();
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
    
        //socket.SetActive(false);
        GameObject tempBullet = Instantiate(balaPrefab, balaTransform.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody>().velocity = balaTransform.forward * fuerza;
        
        IXRSelectInteractable objectEnteringSocket = sX.GetOldestInteractableSelected();
        Destroy(objectEnteringSocket.transform.gameObject);


    }
}
