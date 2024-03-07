using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnterScore : MonoBehaviour
{
    int score;
    string playerName;

    public Button enterName;
    [SerializeField] InputField enterField;
    public TMP_Text scoreInput;
    public Text nameText;
    [SerializeField] QuizManager quizManager;

    [SerializeField] dreamloLeaderBoard dreamLo;
    [SerializeField] HighScores highScore;

    // Start is called before the first frame update
    void Start()
    {

        enterName.onClick.AddListener(LeaderboardInput);
    }

    // Update is called once per frame
    void Update()
    {
        score = System.Convert.ToInt32(quizManager.score);
        playerName = nameText.text;
    }

    void LeaderboardInput()
    {
        Debug.Log(name + score);
        SceneManager.LoadScene("Leaderboard");
        HighScores.UploadScore(playerName, score);
        highScore.UploadNameScore(playerName, score);
        dreamLo.AddScore(name, score);

        if (name == "")
        {
            Debug.Log("Name null");
        }
    }
}
