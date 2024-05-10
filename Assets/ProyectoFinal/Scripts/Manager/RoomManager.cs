using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    private int currentSceneIndex = 0;

    public void UnlockNextRoom()
    {
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            currentSceneIndex++;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
