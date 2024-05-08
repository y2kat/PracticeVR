using UnityEngine;
using UnityEngine.Video;

public class LeverPuzzle : MonoBehaviour
{
    public Lever[] levers;
    public AudioClip victorySound; 
    public GameObject videoObject;
    public GameObject[] objectsToActivate;
    private int nextLeverIndex = 0;
    private AudioSource audioSource;
    public VideoPlayer videoPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = victorySound;
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (nextLeverIndex < levers.Length && levers[nextLeverIndex].isActive)
        {
            nextLeverIndex++;
            if (nextLeverIndex >= levers.Length)
            {
                Debug.Log("¡Puzzle resuelto!");
                audioSource.Play(); 
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoObject.SetActive(false);

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
