using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class EnterScore : MonoBehaviour
{
    public float score;
    public string name;
    public Button enterName;
    [SerializeField] InputField enterField;
    public TMP_Text scoreInput;
    [SerializeField] QuizManager quizManager;

    [SerializeField] dreamloLeaderBoard dreamLo;
    // Start is called before the first frame update
    void Start()
    {
        score = quizManager.score;
        name = enterField.text;
        enterName.onClick.AddListener(LeaderboardInput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LeaderboardInput()
    {
        SceneManager.LoadScene("Leaderboard");
        //HighScores.UploadScore(name, score);
        dreamLo.AddScore(name, score);
    }
}
