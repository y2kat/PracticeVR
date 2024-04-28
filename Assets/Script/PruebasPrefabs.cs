using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Content.Interaction;

public class PruebasPrefabs : MonoBehaviour
{
    public float sliderValue;
    public XRSlider reference;
    public GameObject vaca;
    public GameObject socket;
    public Transform vacaTransform;

    [Header("Cañones")]
    [SerializeField]
    private float fuerza = 10;
    public GameObject balaPrefab;
    public Rigidbody bala;
    public Transform balaTransform;


    // Start is called before the first frame update
    void Start()
    {
        reference = GetComponent<XRSlider>();
    }

    // Update is called once per frame
    void Update()
    {
 
       /* if(reference.value == 1) {
            print("Valor Positivo");
        }   else if (reference.value == 0) {
            print("Valor Negativo");

        }*/

    }

    public void MovX() {
        socket.SetActive(true);    
        print("Movimiento en X");
    }

    public void MovY() {
        socket.SetActive(false);
        print("Movimiento en Y");
    }

    public void ButtonPress (){
        VacaLoca();
        print("Button press");
    }

    public void ValueChange() {
        print("Value Change");
    }

    public void VacaLoca() {
        Instantiate(vaca,vacaTransform.position,Quaternion.identity);
    }

    public void Socket() {
       // Instantiate(socket, transformSocket.position, Quaternion.identity);
    }

    public void Shoot() {

        //socket.SetActive(false);
        Instantiate(balaPrefab, balaTransform.position, Quaternion.identity);
        bala.AddForce(transform.forward * fuerza);
        

    }
}
