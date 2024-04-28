using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private float time = 3f;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ghost") 
        {
            Debug.Log("Colisión con Fantasma");
            Destroy(other.gameObject);
            Destroy(gameObject);
            GameManager.instance.enemiesDefeated++;
        } 
    }
}
