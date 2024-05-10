using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public int trophyCount = 0;
    public int totalTrophies = 5;
    public TextMeshProUGUI trophyCountText;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateTrophyCountText();
    }

    public void AddTrophy()
    {
        trophyCount++;
        UpdateTrophyCountText();
    }

    private void UpdateTrophyCountText()
    {
        trophyCountText.text = trophyCount.ToString() + "/" + totalTrophies.ToString();
    }
}
