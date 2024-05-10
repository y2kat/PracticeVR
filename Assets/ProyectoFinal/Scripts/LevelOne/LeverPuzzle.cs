using UnityEngine;
using UnityEngine.Video;

public class LeverPuzzle : MonoBehaviour
{
    public Lever[] levers;
    public AudioClip victorySound; 
    public GameObject videoObject;
    public GameObject[] objectsToActivate;
    public GameObject objectToDeactivate;
    private int nextLeverIndex = 0;
    private AudioSource audioSource;
    public VideoPlayer videoPlayer;

    [Header ("Objetos de Victoria")]
    public GameObject winObjectToActivate;
    public GameObject[] winObjectToDeactivate;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = victorySound;
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        PuzzleSolved();
    }

    void PuzzleSolved()
    {
        if (nextLeverIndex < levers.Length && levers[nextLeverIndex].isActive)
        {
            nextLeverIndex++;
            if (nextLeverIndex >= levers.Length)
            {
                Debug.Log("RESOLVIDO Bv");
                audioSource.Play();

                //objetos por activar
                winObjectToActivate.SetActive(true);

                //objetos por desactivar
                foreach (GameObject obj in winObjectToDeactivate)
                {
                    obj.SetActive(false);
                }
            }
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoObject.SetActive(false);
        objectToDeactivate.SetActive(false);

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
