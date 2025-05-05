using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    DialogueUI dialogueUI;
    FlappyUI flappyUI;
    private UIState currentState;

    private void Awake()
    {
        dialogueUI = GetComponentInChildren<DialogueUI>(true);
        dialogueUI.Init(this);

        flappyUI = GetComponentInChildren<FlappyUI>(true);
        flappyUI.Init(this);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }
    public void ReturnTown()
    {
        ChangeState(UIState.None);
    }
    public void SetOndialogue(string[] dialogue)
    {
        dialogueUI.Init(dialogue);
        ChangeState(UIState.Dialogue);
    }
    public void SetPlayFlappyGame()
    {
        ChangeState(UIState.Flappy);
    }

    public void FlappyGameUpdateScore(int score)
    {
        flappyUI.UpdateScore(score);
    }
    public void FlappyGameSetRestart()
    {
        flappyUI.SetRestart();
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        dialogueUI.SetActive(currentState);
        flappyUI.SetActive(currentState);
    }
}