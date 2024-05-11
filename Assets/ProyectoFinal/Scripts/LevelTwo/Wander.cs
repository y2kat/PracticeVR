using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 50; // La velocidad de rotaci�n
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = GetRandomPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(targetPosition, transform.position) < 1)
            targetPosition = GetRandomPosition();

        // Calcula la direcci�n hacia la que el agente debe moverse
        Vector3 direction = targetPosition - transform.position;

        // Crea una rotaci�n que mire en la direcci�n del movimiento
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Suavemente rota el agente hacia la direcci�n del movimiento
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Mueve el agente hacia la posici�n objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
    }
}


