using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    DialogueUI dialogueUI;
    FlappyUI flappyUI;
    LeaderboardUI leaderboardUI;
    private UIState currentState;

    public  void init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        dialogueUI = GetComponentInChildren<DialogueUI>(true);
        dialogueUI.Init(this);

        flappyUI = GetComponentInChildren<FlappyUI>(true);
        flappyUI.Init(this);

        leaderboardUI = GetComponentInChildren<LeaderboardUI>(true);

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
    public void SetOndialogue(string[] dialogue , MiniGame miniGame)
    {
        dialogueUI.Init(dialogue , miniGame);
        ChangeState(UIState.Dialogue);
    }
    public void SetoOffdialogue()
    {
        dialogueUI.SetActive(UIState.None);
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
        Debug.Log(currentState.ToString());
        dialogueUI.SetActive(currentState);
        flappyUI.SetActive(currentState);
    }
}