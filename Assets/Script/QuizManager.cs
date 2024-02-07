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
    private int questionCount;
    public string jsonUrl;
    public ScoreManager scoreManager;

    private List<Button> answerButtons = new List<Button>();
    private List<int> Questions = new List<int>();
    private List<int> Buttons = new List<int>();
    public QuizData quizData;
    private int currentQuestionIndex;
    int answersIndex;


    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject questionPanel;

    float correctCount;
    float score;
    string scoreDisplay;
    [SerializeField] TMP_Text scoreText;
    int questionAnswered;
    private void Start()
    {
        
        StartCoroutine(LoadQuizData(jsonUrl));
    }

    private void Update()
    {
        
        scoreDisplay = score.ToString();
        scoreText.SetText(scoreDisplay);
    }

    IEnumerator LoadQuizData(string jsonUrl)
    {
        UnityWebRequest request = UnityWebRequest.Get($"https://shorturl.at/{jsonUrl}");
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
        for (int i = 0; i < questionCount; i++)
        {
            Questions.Add(i);
        }

        Debug.Log("Random" + Questions.Count);

        quizData = JsonUtility.FromJson<QuizData>(jsonText);

        for (int i = 0; i < 5; i++)
        {
            Questions.Add(i);
        }
        questionCount = quizData.soalsoal.Length;

        Debug.Log("number of question" + questionCount);
        scorePanel.SetActive(false);
        questionPanel.SetActive(true);
        DisplayQuestion();

        


    }

    void DisplayQuestion()
    {
        if (quizData != null)
        {
            currentQuestionIndex = Questions[Random.Range(0,Questions.Count)];
            Questions.Remove(currentQuestionIndex);

            SoalData currentQuestion = quizData.soalsoal[currentQuestionIndex];
            questionText.text = currentQuestion.soal;
            ClearAnswerButtons();
            int numberOfAnswer = currentQuestion.jawaban.Length;

            for (int a = 0; a < numberOfAnswer; a++)
            {
                Buttons.Add(a);
            }

            for (int i = 0; i < numberOfAnswer; i++)
            {
                answersIndex = Buttons[Random.Range(0, Buttons.Count)];
                Buttons.Remove(answersIndex);

                JawabanData answerData = currentQuestion.jawaban[answersIndex];
                Button answerButton = Instantiate(answerButtonPrefab, answerButtonParent);
                answerButton.GetComponentInChildren<TextMeshProUGUI>().text = answerData.jawaban;

                
                int index = i;
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
        
        Debug.Log("Selected Index: " + selectedIndex + ", Correct: " + isCorrect);


        if (isCorrect)
        {
            correctAnswer();
        }

      
       
       
        Debug.Log("QIndex = " + currentQuestionIndex);
        Debug.Log("score " + score);

        questionAnswered++;
        if (questionAnswered > questionCount - 1)
        {
            Debug.Log("End of Quiz");
            scorePanel.SetActive(true);
            questionPanel.SetActive(false);
           
        }
        else
        {
           Invoke(nameof(DisplayQuestion), 1f);
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
