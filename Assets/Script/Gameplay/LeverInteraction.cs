using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LeverInteraction : MonoBehaviour
{
    public GameObject portalApagado; 
    private XRBaseInteractable palancaInteractable;
    private bool portalEncendido = false; // Estado del objeto

    private void Start()
    {
        palancaInteractable = GetComponent<XRBaseInteractable>();

        palancaInteractable.selectEntered.AddListener(ToggleObjeto);
    }

    private void ToggleObjeto(SelectEnterEventArgs args)
    {
        // Cambia el estado del objeto (encendido/apagado)
        portalEncendido = !portalEncendido;

        // Activa o desactiva el objeto según su estado
        portalApagado.SetActive(portalEncendido);

    }
}

