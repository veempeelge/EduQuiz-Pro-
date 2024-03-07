using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public State gameState;
    [SerializeField] GameObject codePanel;
    [SerializeField] GameObject quiz;
    [SerializeField] GameObject finishQuiz;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void SwitchState(State newState)
    {
        if (gameState == newState) return;

        gameState = newState;

        switch (gameState)
        {
            case State.EnterCode:
                EnterCode();
                break;
            case State.Quiz:
                Quiz();
                break;
            case State.FinishQuiz:
                FinishQuiz();
                break;
        }
    }

   

    private void EnterCode()
    {
        quiz.SetActive(false);
        finishQuiz.SetActive(false);
    }

    private void Quiz()
    {
        quiz.SetActive(true);
        codePanel.SetActive(false);
    }
    private void FinishQuiz()
    {
        finishQuiz.SetActive(true);
        quiz.SetActive(false);
    }

}

public enum State
{
    EnterCode,
    Quiz,
    FinishQuiz
}