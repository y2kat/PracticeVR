using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class QuizGame : MonoBehaviour
{
    public Image initialImage;
    public List<Image> questionImages; // Esto ahora es una lista
    public XRBaseInteractable buttonA;
    public XRBaseInteractable buttonB;
    public XRBaseInteractable buttonC;
    public AudioSource incorrectSound;

    private int currentQuestion = 0;
    private List<Question> questions;

    public GameObject objectToActivate;

    void Start()
    {
        // Muestra la imagen inicial
        initialImage.gameObject.SetActive(true);

        // Inicializa las preguntas aquí
        questions = new List<Question>();
        questions.Add(new Question(questionImages[0], 1)); // Pregunta 1, respuesta B
        questions.Add(new Question(questionImages[1], 2)); // Pregunta 2, respuesta C
        questions.Add(new Question(questionImages[2], 2)); // Pregunta 3, respuesta C
        questions.Add(new Question(questionImages[3], 2)); // Pregunta 4, respuesta C

        // Desactiva todas las imágenes al inicio
        foreach (Image image in questionImages)
        {
            image.gameObject.SetActive(false);
        }

        // Configura los botones para llamar a la función CheckAnswer cuando se presionen
        buttonA.selectEntered.AddListener((XRBaseInteractor) => CheckAnswer(0));
        buttonB.selectEntered.AddListener((XRBaseInteractor) => CheckAnswer(1));
        buttonC.selectEntered.AddListener((XRBaseInteractor) => CheckAnswer(2));

        // Espera un tiempo y luego comienza el juego
        StartCoroutine(StartGameAfterDelay(8.0f));  // Espera 5 segundos antes de comenzar
    }

    IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Oculta la imagen inicial y muestra la primera pregunta
        initialImage.gameObject.SetActive(false);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        // Muestra la imagen de la pregunta actual en el Canvas
        questionImages[currentQuestion].gameObject.SetActive(true);
    }

    void CheckAnswer(int answer)
    {
        // Comprueba si la respuesta es correcta
        if (answer == questions[currentQuestion].correctAnswer)
        {
            // Si es correcta, pasa a la siguiente pregunta
            questionImages[currentQuestion].gameObject.SetActive(false);
            currentQuestion++;
            if (currentQuestion < questions.Count)
            {
                ShowQuestion();
            }
            else
            {
                // Si el jugador ha respondido correctamente a todas las preguntas, activa el objeto
                objectToActivate.SetActive(true);
            }
        }
        else
        {
            // Si es incorrecta, reproduce un sonido
            incorrectSound.Play();
        }
    }
}

public class Question
{
    public Image questionImage;
    public int correctAnswer;

    public Question(Image questionImage, int correctAnswer)
    {
        this.questionImage = questionImage;
        this.correctAnswer = correctAnswer;
    }
}


