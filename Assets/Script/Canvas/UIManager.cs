using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour //source: CHATGPT uwu
{
    public TextMeshProUGUI enemiesDefeatedText; // Referencia al texto de enemigos derrotados
    public TextMeshProUGUI timeElapsedText; // Referencia al texto de tiempo transcurrido

    void Update()
    {
        // Actualiza el texto de enemigos derrotados y tiempo transcurrido
        enemiesDefeatedText.text = GameManager.instance.enemiesDefeated + "/10";
        timeElapsedText.text = "Time: " + Mathf.Round(GameManager.instance.timeElapsed) + "s";
    }
}

