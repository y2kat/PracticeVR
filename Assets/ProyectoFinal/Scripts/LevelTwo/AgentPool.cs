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

    private void Awake()
    {
        instance = this;

        agentPools = new Dictionary<string, Queue<GameObject>>();

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
        if (agentPools[id].Count > 0)
        {
            GameObject agent = agentPools[id].Dequeue();
            agent.SetActive(true);
            return agent;
        }
        else
        {
            // Si no hay agentes disponibles en el pool, instanciamos uno nuevo
            return Instantiate(agentPrefabs.First(prefab => prefab.GetComponent<Agent>().id == id));
        }
    }

    public void ReturnAgent(GameObject agent)
    {
        agent.SetActive(false);
        agentPools[agent.GetComponent<Agent>().id].Enqueue(agent);
    }
}


