using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Web;

public class EnterScore : MonoBehaviour
{
    float score;
    string playerName;

    public Button enterName;
    [SerializeField] TMP_InputField enterField;
    public TMP_Text scoreInput;
    public TMP_Text nameText;
    [SerializeField] QuizManager quizManager;

    [SerializeField] dreamloLeaderBoard dreamLo;
    [SerializeField] HighScores highScore;

    // Start is called before the first frame update
    void Start()
    {
        enterField.onValueChanged.AddListener(InputChanged);
        enterName.onClick.AddListener(LeaderboardInput);
    }

    private void InputChanged(string newName)
    {
        score = quizManager.score;
        playerName = HttpUtility.UrlEncode(newName);
        Debug.Log(playerName);
    }

    void LeaderboardInput()
    {
        Debug.Log(name + score);
        SceneManager.LoadScene("Leaderboard");
        // HighScores.UploadScore(playerName, score);
        highScore.UploadNameScore(playerName, score);
        dreamLo.AddScore(name, score);

        if (name == "")
        {
            Debug.Log("Name null");
        }
    }
}
