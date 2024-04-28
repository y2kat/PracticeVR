using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarraVida : MonoBehaviour
{
    public float vida = 100;
    public Image barraVida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vida = Mathf.Clamp(vida, 0, 100);
        barraVida.fillAmount = vida/100;

        if(vida <= 0)
        {
           CanvasManager.referenceCanvas.GameOver();
           Destroy(gameObject); 
        }
    }
}
