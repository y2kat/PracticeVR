using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour // Source: CHATGPT

{
       public GameObject enemyPrefab; // Prefab del enemigo
    public Transform playerTransform; // Transform del jugador
    public float spawnInterval = 3f; // Intervalo de tiempo entre cada spawn
    public int maxEnemies = 5; // Límite máximo de enemigos
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    private int currentEnemyCount = 0; // Contador de enemigos actuales

    private void Start()
    {
        // Comienza a spawnear enemigos
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Verifica si se ha alcanzado el límite máximo de enemigos
            if (currentEnemyCount < maxEnemies)
            {
                // Instancia un nuevo enemigo
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

                // Obtén el componente Enemy del nuevo enemigo
                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();

                // Establece el transform del jugador en el enemigo
                enemyComponent.SetPlayerTransform(playerTransform);

                // Incrementa el contador de enemigos
                currentEnemyCount++;
            }

            // Espera el tiempo de intervalo antes de spawnear otro enemigo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecreaseEnemyCount()
    {
        currentEnemyCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
