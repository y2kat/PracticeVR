using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int trophyCount = 0;
    public int totalTrophies = 5;
    public TextMeshProUGUI trophyCountText;

    public GameObject[] objectsToActivate;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateTrophyCountText();
    }

    public void AddTrophy()
    {
        if (trophyCount < totalTrophies)
        {
            trophyCount++;
            UpdateTrophyCountText();
        }

        if (trophyCount == totalTrophies)
        {
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
        }
    }

    private void UpdateTrophyCountText()
    {
        trophyCountText.text = trophyCount.ToString() + "/" + totalTrophies.ToString();
    }
}
