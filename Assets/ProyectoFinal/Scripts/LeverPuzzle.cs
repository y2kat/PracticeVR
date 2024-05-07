using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    public Lever[] levers;
    private List<Lever> activatedLevers = new List<Lever>();

    void Update()
    {
        foreach (Lever lever in levers)
        {
            if (lever.isActive && !activatedLevers.Contains(lever))
            {
                activatedLevers.Add(lever);
                StartCoroutine(CheckSolutionWithDelay());
            }
        }
    }

    IEnumerator CheckSolutionWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        if (CheckSolution())
        {
            Debug.Log("lo resolviste pá :v");
        }
    }

    bool CheckSolution()
    {
        for (int i = 0; i < activatedLevers.Count; i++)
        {
            if (activatedLevers[i] != levers[i])
            {
                return false;
            }
        }
        return activatedLevers.Count == levers.Length;
    }
}
