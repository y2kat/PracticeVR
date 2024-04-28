using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia estática para acceder al GameManager desde otros scripts

    public int enemiesDefeated = 0; // Cuenta de enemigos derrotados
    public float timeElapsed = 0f; // Tiempo transcurrido desde el inicio del juego

    private bool gameEnded = false; // Indica si el juego ha terminado

    void Awake()
    {
        // Asegura que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Si el juego no ha terminado, incrementa el tiempo transcurrido
        if (!gameEnded)
        {
            timeElapsed += Time.deltaTime;
        }

        // Si se han derrotado 40 enemigos, termina el juego
        if (enemiesDefeated >= 10)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        gameEnded = true;
        Debug.Log("¡Has ganado el juego! Tiempo transcurrido: " + timeElapsed + " segundos");
        CanvasManager.referenceCanvas.GameOver();

        // Guarda el highscore y el mejor tiempo si son mejores que los actuales
        if (enemiesDefeated > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", enemiesDefeated);
        }
        if (timeElapsed < PlayerPrefs.GetFloat("BestTime", float.MaxValue))
        {
            PlayerPrefs.SetFloat("BestTime", timeElapsed);
        }

        PlayerPrefs.Save();
    }
}
