using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEntryUi : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text numberDisplay, nameDisplay, scoreDisplay;
    [SerializeField] private Image bg;

    public void Setup(int number, string name, float score, Color bgColor)
    {
        numberDisplay.SetText(number.ToString("00"));
        nameDisplay.SetText(HttpUtility.UrlDecode(name));
        scoreDisplay.SetText(score.ToString());
        bg.color = bgColor;
    }
}
