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
        if (other.gameObject.tag == "Agent")
        {
            Debug.Log("Colisión con Agent");
            other.gameObject.GetComponent<Agent>().OnShot();
            Destroy(gameObject);
        }
    }

}
