using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour 
{
    public TMPro.TextMeshProUGUI[] rNames;
    public TMPro.TextMeshProUGUI[] rScores;

    [SerializeField] TextMeshProUGUI textPrefabs, firstText, secondText, thirdText;
    [SerializeField] TextMeshProUGUI scorePrefabs, firstScore, secondScore, thirdScore;


    [SerializeField] Transform nameTextParent;
    [SerializeField] Transform scoreTextParent;

    public List<int> Names = new List<int>();
    public List<int> Scores = new List<int>();

    HighScores myScores;

    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". Fetching...";
        }
        myScores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int a = 0; a < highscoreList.Length; a++)
        {
            Names.Add(a);
            Scores.Add(a);
        }

        for (int i = 0; i < Names.Count;i ++)
        {
            string stringFormat = "0.0";
            TMP_Text nameText = Instantiate(textPrefabs, nameTextParent);
            TMP_Text scoreText = Instantiate(scorePrefabs, scoreTextParent);


            nameText.text = highscoreList[i].username;
            scoreText.text = highscoreList[i].score.ToString(stringFormat);

            if (i == 0)
            {
                firstText.text = highscoreList[i].username;
                firstScore.text = highscoreList[i].score.ToString(stringFormat);
            }

            else if (i == 1)
            {
                secondText.text = highscoreList[i].username;
                secondScore.text = highscoreList[i].score.ToString(stringFormat);
            }

            else if (i == 2)
            {
                thirdText.text = highscoreList[i].username;
                thirdScore.text = highscoreList[i].score.ToString(stringFormat);
            }
            

               // rNames[i].text = i + 1 + ". ";
               // if (highscoreList.Length < i)
                {
                  // rScores[i].text = highscoreList[i].score.ToString();
                  // rNames[i].text = highscoreList[i].username;

                }
            
        }
    }

    public void AddList()
    {
       
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
