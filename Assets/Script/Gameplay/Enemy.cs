using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] float speed = 10f;

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.player = playerTransform;
    }

    void Start()
    {
        agent.speed = speed;
    }

    void Update()
    {
        if (player != null)
        {
            // Establece la posici�n del jugador como destino del agente
            agent.SetDestination(player.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Colisi�n con Jugador");
        }

    }
}
