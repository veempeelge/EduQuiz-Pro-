using JetBrains.Annotations;
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
    public TMPro.TextMeshProUGUI[] rNumbers;

    [SerializeField] ScoreEntryUi scoreEntryUiPrefab;
    [SerializeField] RectTransform scrollContainer;
    [SerializeField] TextMeshProUGUI textPrefabs, firstText, secondText, thirdText, numbersPrefabs;
    [SerializeField] TextMeshProUGUI scorePrefabs, firstScore, secondScore, thirdScore;
    [SerializeField] GameObject boxPrefab;

    [SerializeField] Transform nameTextParent;
    [SerializeField] Transform scoreTextParent;
    [SerializeField] Transform numberParent;
    [SerializeField] Transform boxParent;

    public List<int> Names = new List<int>();
    public List<int> Scores = new List<int>();
    public List<int> Numbers = new List<int>();

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
            Numbers.Add(a);
            Names.Add(a);
            Scores.Add(a);

        }

        for (int x = 0; x < Names.Count - 3; x++)
        {
            GameObject boxObject = Instantiate(boxPrefab, boxParent);
        }

        for (int i = 0; i < Names.Count;i ++)
        {
            //string stringFormat = "0.0";
            int numbers = i + 1;
            //TMP_Text nameText = Instantiate(textPrefabs, nameTextParent);
            //TMP_Text scoreText = Instantiate(scorePrefabs, scoreTextParent);
            //TMP_Text numberText = Instantiate(numbersPrefabs, numberParent);
            var entry = Instantiate(scoreEntryUiPrefab, scrollContainer);
            var color = new Color(0.3f, 0.3f, 0.3f, 0.3f);

            if (i == 0)
            {
                color = Color.red;
            }
            else if (i == 1)
            {
                color = Color.green;
            }
            else if (i == 2)
            {
                color = Color.blue;
            }

            entry.Setup(numbers, highscoreList[i].username, highscoreList[i].score, color);

            //nameText.text = highscoreList[i].username;
            //scoreText.text = highscoreList[i].score.ToString(stringFormat);
            //numberText.text = numbers.ToString();

            //if (i == 0)
            //{
            //    firstText.text = highscoreList[i].username;
            //    firstScore.text = highscoreList[i].score.ToString(stringFormat);
            //}

            //else if (i == 1)
            //{
            //    secondText.text = highscoreList[i].username;
            //    secondScore.text = highscoreList[i].score.ToString(stringFormat);
            //}

            //else if (i == 2)
            //{
            //    thirdText.text = highscoreList[i].username;
            //    thirdScore.text = highscoreList[i].score.ToString(stringFormat);
            //}
            

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
    IEnumerator RefreshHighscores()
    {
        while(true)
        {
            Names.Clear();
            Numbers.Clear();
            Scores.Clear();

        
            DestroyChildren(nameTextParent);
            DestroyChildren(scoreTextParent);
            DestroyChildren(numberParent);

            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }

    void DestroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
