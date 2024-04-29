using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonDoor : MonoBehaviour
{
    public Animator puertaAnimator;
    private XRBaseInteractable botonInteractable;

    private void Start()
    {
        // Obt�n el componente XRBaseInteractable del bot�n
        botonInteractable = GetComponent<XRBaseInteractable>();

        // Suscr�bete al evento de presionar el bot�n
        botonInteractable.selectEntered.AddListener(ActivarAnimacionPuerta);
    }

    private void ActivarAnimacionPuerta(SelectEnterEventArgs args)
    {
        // Activa la animaci�n de la puerta
        puertaAnimator.SetTrigger("open");

        // Tambi�n puedes reproducir un sonido o realizar otras acciones aqu� si lo deseas
    }
}
