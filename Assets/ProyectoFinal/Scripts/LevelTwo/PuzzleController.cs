using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PuzzleController : MonoBehaviour
{
    public static PuzzleController instance;

    private List<string> sequence = new List<string> { "4", "-", "4" };
    private int currentIndex = 0; // El índice del próximo agente en la secuencia

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Dictionary<string, int> agentCounts = new Dictionary<string, int>
        {
            {"1", 2},
            {"2", 2},
            {"3", 2},
            {"4", 2},
            {"5", 2},
            {"0", 10},
            {"-", 2}
        };

        // Instancia la cantidad especificada de cada tipo de agente
        foreach (var pair in agentCounts)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                GameObject agent = AgentPool.instance.GetAgent(pair.Key);
                agent.transform.position = GetRandomPosition();
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10, 10);
        float y = 1;
        float z = Random.Range(-10, 10);

        return new Vector3(x, y, z);
    }

    public bool IsNextInSequence(string id)
    {
        return id == sequence[currentIndex];
    }

    public void RegisterHit(Agent agent)
    {
        currentIndex++;

        if (currentIndex == sequence.Count)
        {
            // El jugador ha ganado el puzzle
            Debug.Log("¡Has ganado el puzzle!");
        }
    }

    public void RegisterMiss(Agent agent)
    {
        // El jugador ha fallado, reiniciar el puzzle
        Debug.Log("Has fallado, vuelve a intentarlo.");
        currentIndex = 0;

        // Reiniciar el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


