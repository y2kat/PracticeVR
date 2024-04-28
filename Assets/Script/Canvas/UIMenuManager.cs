using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public void StartGame() 
    {
        try { SceneManager.LoadScene("SampleScene"); }
        catch (Exception e) { Debug.LogError("Problem Scene" + e.Message); }
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
