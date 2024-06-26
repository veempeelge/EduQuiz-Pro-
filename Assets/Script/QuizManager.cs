using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button answerButtonPrefab;
    public Sprite[] buttonSprite;
    public Transform answerButtonParent;
    private int questionCount;
    public string code;
    public ScoreManager scoreManager;

    private List<Button> answerButtons = new List<Button>();
    private List<int> Questions = new List<int>();
    private List<int> Buttons = new List<int>();
    public QuizData quizData;
    private int currentQuestionIndex;
    int answersIndex;


    [SerializeField] GameObject scorePanel;
    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject inputCodePanel;
    [SerializeField] GameObject previewButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] TMP_Text title;

    float correctCount;
    public float score;
    string scoreDisplay;
    [SerializeField] TMP_Text scoreText;
    int questionAnswered;

    bool isClicked;
    [SerializeField] TMPro.TMP_InputField input;

    private Button _clickedButton;
    [SerializeField] Button enterButton;
    string url;

    DropDown dropDown;

    float timer;
    [SerializeField] float maxTimer;
    [SerializeField] Image timerImage;
    [SerializeField] TMP_Text QCountDisplay;

    bool linkWorked;

    [SerializeField] GameState gameState;
    [SerializeField] private GameObject tryAgain;
    [SerializeField] private GameObject noConnection;
    [SerializeField] private GameObject invalidCode;



    public bool correctAns;
    CustomInteractable customInt;

    [SerializeField] Slider loadingProgressBar;
    bool gameStarted = false;

    [SerializeField] GameObject timesUpPanel;

    private void Start()
    {
        loadingScreen.SetActive(false);
        previewButton.SetActive(false);
        scorePanel.SetActive(false);
        questionPanel.SetActive(false);
        tryAgain.SetActive(false);
        noConnection.SetActive(false);
        timesUpPanel.SetActive(false);
        invalidCode.SetActive(false);
        //gameState.SwitchState(State.EnterCode);
        // input.onEndEdit.AddListener(Validate);
        enterButton.onClick.AddListener(Validate);
        customInt = FindAnyObjectByType<CustomInteractable>();
    }

    private void Update()
    {
        scoreDisplay = score.ToString();
        scoreText.SetText(scoreDisplay);

        if (!isClicked)
        {
            timer += 1f * Time.deltaTime;
            foreach (Button t in answerButtons)
            {
                t.interactable = true;
            }
        }
        else
        {
            foreach (Button t in answerButtons)
            {
                t.interactable = false;
            }
        }

        if (gameStarted)
        {
            if (timer > maxTimer)
            {

                isClicked = true;
                timesUpPanel.SetActive(true);
                unanswered();
               // Invoke(nameof(HideTimesUp),2f);
            }
        }

        timerImage.fillAmount = timer / maxTimer;
    }

    void StartGame()
    {
        timer = 0;
    }

    void unanswered()
    {
        Invoke(nameof(DisplayQuestion), 2f);
        questionAnswered++;
        Debug.Log("unanswered");
        timer = 0;
    }
    IEnumerator LoadQuizData(string code)
    {
        UnityWebRequest request = UnityWebRequest.Get(code);
        float startTime = Time.time;

        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            loadingScreen.SetActive(false);
            string jsonText = request.downloadHandler.text;
            ParseAndDisplayQuiz(jsonText);
        }
        else
        {
            loadingScreen.SetActive(false);
            enterButton.interactable = true;

            Debug.Log(code);
            Debug.Log("Failed to load JSON: " + request.error);
        }
    }

    void UpdateLoadingBar(UnityWebRequest request)
    {
        // Update the loading bar value based on the download progress
        loadingProgressBar.value = request.downloadProgress/1;
    }

    void ParseAndDisplayQuiz(string jsonText)
    {

        if (IsValidJson(jsonText))
        {
            gameStarted = true;
            timer = 0;
            for (int i = 0; i < questionCount; i++)
            {
                Questions.Add(i);
            }

            // Debug.Log("Random" + Questions.Count);

            quizData = JsonUtility.FromJson<QuizData>(jsonText);
            title.text = quizData.quiztitle;
            questionCount = quizData.soalsoal.Length;
            for (int i = 0; i < questionCount; i++)
            {
                Questions.Add(i);
            }
            // Debug.Log("number of question" + questionCount);
            scorePanel.SetActive(false);
            questionPanel.SetActive(true);

            gameState.SwitchState(State.Quiz);
            DisplayQuestion();
            tryAgain.SetActive(false);
            noConnection.SetActive(false);
            loadingScreen.SetActive(false);
        }
        else
        {
            enterButton.interactable = true;
            loadingScreen.SetActive(false);
            tryAgain.SetActive(true);
            Debug.Log("Invalid JSON");
        }
        
    }
    private void Validate()
    {
        enterButton.interactable = false;
        loadingProgressBar.value = 0;
        loadingScreen.SetActive(true);
        code = input.text;
        url = $"https://api.npoint.io/{code}";
        //Debug.Log(url);
       
        var req = UnityWebRequest.Get(url);

        //StartCoroutine(LoadQuizData(url));
        StartCoroutine(UpdateLoadingBarCoroutine(url));

        req.SendWebRequest().completed += op =>
        {
            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    StartCoroutine(LoadQuizData(url));
                    loadingScreen.SetActive(false);
                    enterButton.interactable = true;

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    noConnection.SetActive(true);
                    invalidCode.SetActive(false);
                    loadingScreen.SetActive(false);
                    enterButton.interactable = true;

                    break;
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log("Invalid Code");
                    invalidCode.SetActive(true);
                    noConnection.SetActive(false);

                    loadingScreen.SetActive(false);
                    enterButton.interactable = true;

                    break;
            }
        };
    }

    IEnumerator UpdateLoadingBarCoroutine(string code)
    {
        UnityWebRequest request = UnityWebRequest.Get(code);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request and wait until it's completed
        yield return request.SendWebRequest();

        while (!request.isDone)
        {
            UpdateLoadingBar(request);
            yield return null;
        }
    }

    bool IsValidJson(string jsonString)
    {
        try
        {
            // Attempt to parse the JSON string
            var jsonData = JsonUtility.FromJson<object>(jsonString);
            return true; // Parsing succeeded, so it's valid JSON
        }
        catch (Exception)
        {
            return false; // Parsing failed, so it's not valid JSON
        }
    }

    void DisplayQuestion()
    {
        timesUpPanel.SetActive(false);
        loadingScreen.SetActive(false);
        QCountDisplay.SetText(questionAnswered + 1 + "/" + questionCount);
        timer = 0;
        ClearAnswerButtons();
        isClicked = false;
        if (quizData != null)
        {
           
            currentQuestionIndex = Questions[UnityEngine.Random.Range(0,Questions.Count)];
            //Debug.Log(questionCount);
            Questions.Remove(currentQuestionIndex);

            SoalData currentQuestion = quizData.soalsoal[currentQuestionIndex];
            questionText.text = currentQuestion.soal;
            
            int numberOfAnswer = currentQuestion.jawaban.Length;

            for (int a = 0; a < numberOfAnswer; a++)
            {
                Buttons.Add(a);

                JawabanData answerData = currentQuestion.jawaban[a];

            }

            for (int i = 0; i < numberOfAnswer; i++)
            {
            
                answersIndex = Buttons[UnityEngine.Random.Range(0, Buttons.Count)];
                Buttons.Remove(answersIndex);
                
                JawabanData answerData = currentQuestion.jawaban[answersIndex];
                Button answerButton = Instantiate(answerButtonPrefab, answerButtonParent);
                answerButton.GetComponentInChildren<TextMeshProUGUI>().text = answerData.jawaban;
                answerButton.GetComponent<CustomInteractable>().SetCorrect(answerData.benar);
                int index = i;
                answerButton.onClick.AddListener(() => OnAnswerButtonClicked(index, answerData.benar));
                answerButton.GetComponent<Image>().sprite = buttonSprite[i];

          

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

    public void OnAnswerButtonClicked(int selectedIndex, bool isCorrect)
    {
        if (!isClicked)
        {
            _clickedButton = answerButtons[selectedIndex];
          //  Debug.Log("Selected Index: " + selectedIndex + ", Correct: " + isCorrect);
            

            if (isCorrect)
            {
                
                //_clickedButton.GetComponent<Image>().color = Color.green;
                correctAnswer();
            }
            else
            {
                //_clickedButton.GetComponent<Image>().color = Color.red;
         
                //wrong answer animation
            }



            questionAnswered++;
           
            if (questionAnswered == questionCount)
            {
                Invoke(nameof(QuizEnd), 1f);
            }
            else
            {
                Invoke(nameof(DisplayQuestion), 1f);
            }

            isClicked = true;
        }
        else
        {
            Debug.Log("alr answered");
        }

        

        foreach (Button button in answerButtons)
        {
           if (button != _clickedButton)
           {
                button.GetComponent<Image>().color = Color.grey;
           }
        }

        //answerButtons.Clear();
    }

    void QuizEnd()

    {
        scorePanel.SetActive(true);
        questionPanel.SetActive(false);
        gameState.SwitchState(State.FinishQuiz);
    }

    void correctAnswer()
    {
        correctCount++;
        score = Mathf.RoundToInt((correctCount/(questionCount)*100)*10)/10;
        // Debug.Log("correct : " + correctCount);
        //change to green/ any animation
 
    }

    void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    [System.Serializable]
    public class QuizData
    {
        public SoalData[] soalsoal;
        public string quiztitle;
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
