using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button answerButtonPrefab;
    public Transform answerButtonParent;
    private float questionCount;
    public string jsonUrl;
    public ScoreManager scoreManager;  // Reference to the ScoreManager script (drag it in the Inspector)

    private List<Button> answerButtons = new List<Button>();
    public QuizData quizData;
    private int currentQuestionIndex = 0;


    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject questionPanel;

    float correctCount;
    float score;
    string scoreDisplay;
    [SerializeField] TMP_Text scoreText;

    private void Start()
    {
        StartCoroutine(LoadQuizData());
        
    }

    private void Update()
    {
        
        scoreDisplay = score.ToString();
        scoreText.SetText(scoreDisplay);
    }

    IEnumerator LoadQuizData()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonText = request.downloadHandler.text;
            ParseAndDisplayQuiz(jsonText);
        }
        else
        {
            Debug.LogError("Failed to load JSON: " + request.error);
        }
    }

    void ParseAndDisplayQuiz(string jsonText)
    {
        quizData = JsonUtility.FromJson<QuizData>(jsonText);
        questionCount = quizData.soalsoal.Length;
        Debug.Log("number of question" + questionCount);
        scorePanel.SetActive(false);
        questionPanel.SetActive(true);
        // Display the first question
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (quizData != null && currentQuestionIndex < quizData.soalsoal.Length)
        {
            SoalData currentQuestion = quizData.soalsoal[currentQuestionIndex];

            // Display the question text
            questionText.text = currentQuestion.soal;

            // Clear previous answer buttons
            ClearAnswerButtons();

            // Spawn answer buttons
            for (int i = 0; i < currentQuestion.jawaban.Length; i++)
            {
                JawabanData answerData = currentQuestion.jawaban[i];
                Button answerButton = Instantiate(answerButtonPrefab, answerButtonParent);
                answerButton.GetComponentInChildren<TextMeshProUGUI>().text = answerData.jawaban;

                // Add a listener to the button click event
                int index = i; // Ensure correct capture of the loop variable
                answerButton.onClick.AddListener(() => OnAnswerButtonClicked(index, answerData.benar));

                answerButtons.Add(answerButton);

                
            }
        }
    }

    void ClearAnswerButtons()
    {
        foreach (Button button in answerButtons)
        {
            Destroy(button.gameObject);
        }
        answerButtons.Clear();
    }

    void OnAnswerButtonClicked(int selectedIndex, bool isCorrect)
    {
        // Handle the button click event, e.g., check if the answer is correct
        Debug.Log("Selected Index: " + selectedIndex + ", Correct: " + isCorrect);

        // Increase the score and pass the correctness of the answer

        if (isCorrect)
        {
            correctAnswer();
        }

        // Move to the next question
        currentQuestionIndex++;
        Debug.Log("score " + score);

        // Check if it's the last question
        if (currentQuestionIndex < quizData.soalsoal.Length)
        {
            DisplayQuestion();
        }
        else
        {
            // It's the last question, handle the end of the quiz
            Debug.Log("End of Quiz");
            scorePanel.SetActive(true);
            questionPanel.SetActive(false);
        }
    }

    void correctAnswer()
    {
        correctCount++;
        score = correctCount/(questionCount)*100;
        Debug.Log("correct : " + correctCount);
    }

    [System.Serializable]
    public class QuizData
    {
        public SoalData[] soalsoal;
    }

    [System.Serializable]
    public class SoalData
    {
        public string soal;
        public JawabanData[] jawaban;
    }

    [System.Serializable]
    public class JawabanData
    {
        public bool benar;
        public string jawaban;
    }
}
