using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int correctCount = 0;
    private int totalCount = 0;
    int currentScore;

    string toDisplay;

    [SerializeField] GameObject scorePanel;
    public TMP_Text scoreText;  // Reference to a Text component to display the score (drag it in the Inspector)
    public QuizManager quizManager;  // Reference to the QuizManager script (drag it in the Inspector)

    private void Start()
    {
        // Automatically initialize totalCount based on the number of questions in the JSON file
        totalCount = GetTotalQuestionCount();

        // Hide the score text at the start
        HideScoreText();
    }

    private void Update()
    {
        currentScore = correctCount / totalCount * 100;
        toDisplay = currentScore.ToString();
    }
    // Call this method to increase the player's score
    public void IncreaseScore(bool isCorrect)
    {
        if (isCorrect)
        {
            correctCount++;
        }

        UpdateScoreText();

        // Check if it's the last question
        if (totalCount > 0 && correctCount + 1 == totalCount)
        {
            // Show the score text after the last question
            ShowScoreText();
        }
    }

    // Update the Text component to display the current score
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.SetText(toDisplay);
        }
    }

    // Calculate the total number of questions from the QuizManager
    private int GetTotalQuestionCount()
    {
        if (quizManager != null && quizManager.quizData != null)
        {
            return quizManager.quizData.soalsoal.Length;
        }

        return 0;
    }

    // Method to hide the score text
    private void HideScoreText()
    {
        if (scoreText != null)
        {
            scorePanel.gameObject.SetActive(false);
        }
    }

    // Method to show the score text
    private void ShowScoreText()
    {
        if (scoreText != null)
        {
            scorePanel.gameObject.SetActive(true);
        }
    }
}
