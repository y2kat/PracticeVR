using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject gameOverPanel, cameraPlayer, vidaPanel;
    public TextMeshProUGUI highScoreText; // Nuevo texto para el highscore
    public TextMeshProUGUI bestTimeText; // Nuevo texto para el mejor tiempo
    public static CanvasManager referenceCanvas;

    void Start()
    {
        referenceCanvas = this;
    }
    public void Salir()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Salir");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return null; // Espera hasta el próximo frame
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true); 
        vidaPanel.SetActive(false); 
        cameraPlayer.SetActive(true);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        // Actualiza el texto del highscore y el mejor tiempo
        highScoreText.text = "Highscore: " + GameManager.instance.enemiesDefeated;
        bestTimeText.text = "Best Time: " + Mathf.Round(GameManager.instance.timeElapsed) + "s";
    }
}
