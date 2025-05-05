using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    DialogueUI dialogueUI;
    private UIState currentState;

    private void Awake()
    {
        dialogueUI = GetComponentInChildren<DialogueUI>(true);
        dialogueUI.Init(this);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }



    public void ChangeState(UIState state)
    {
        currentState = state;
        dialogueUI.SetActive(currentState);
    }
}