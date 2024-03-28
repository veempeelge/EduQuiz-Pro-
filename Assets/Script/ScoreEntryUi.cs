using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEntryUi : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text numberDisplay, nameDisplay, scoreDisplay;
    [SerializeField] private Image bg;

    public void Setup(int number, string name, float score, Color bgColor)
    {
        numberDisplay.SetText(number.ToString("00"));
        nameDisplay.SetText(name);
        scoreDisplay.SetText(score.ToString());
        bg.color = bgColor;
    }
}
