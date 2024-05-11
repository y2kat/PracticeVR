using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Asegúrate de tener esta directiva en la parte superior de tu script

public class AgentPool : MonoBehaviour
{
    public static AgentPool instance;

    public GameObject[] agentPrefabs;
    public int poolSize = 10;

    private Dictionary<string, Queue<GameObject>> agentPools;
    public List<GameObject> activeAgents;

    private void Awake()
    {
        instance = this;

        agentPools = new Dictionary<string, Queue<GameObject>>();
        activeAgents = new List<GameObject>();

        foreach (GameObject prefab in agentPrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject agent = Instantiate(prefab);
                agent.SetActive(false);
                pool.Enqueue(agent);
            }

            // Asegúrate de que el id del prefab coincide con el id que estás utilizando para acceder al pool
            agentPools.Add(prefab.GetComponent<Agent>().id, pool);
        }
    }

    public GameObject GetAgent(string id)
    {
        GameObject agent;

        if (agentPools[id].Count > 0)
        {
            agent = agentPools[id].Dequeue();
        }
        else
        {
            // Si no hay agentes disponibles en el pool, instanciamos uno nuevo
            agent = Instantiate(agentPrefabs.First(prefab => prefab.GetComponent<Agent>().id == id));
        }

        agent.SetActive(true);
        activeAgents.Add(agent); // Añade el agente a la lista de agentes activos
        return agent;
    }

    public void ReturnAgent(GameObject agent)
    {
        agent.SetActive(false);
        agentPools[agent.GetComponent<Agent>().id].Enqueue(agent);
        activeAgents.Remove(agent); // Elimina el agente de la lista de agentes activos
    }

    public void DestroyAllAgents()
    {
        // Destruye todos los agentes activos
        foreach (GameObject agent in activeAgents)
        {
            Destroy(agent);
        }

        // Limpia la lista de agentes activos
        activeAgents.Clear();
    }
}


