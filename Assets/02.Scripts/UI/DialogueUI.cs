using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogueUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button NextButton;

    string[] dialogues = null;
    int nextTextCount = 0;
    MiniGame miniGame;
    protected override UIState GetUIState()
    {
        return UIState.Dialogue;
    }
    public void Init(string[] dialogues , MiniGame miniGame)
    {
        this.dialogues = null;
        this.dialogues = dialogues;
        this.miniGame = miniGame;
        nextTextCount = 0;
        UpdateNaxtText();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateNaxtText()
    {
        if (nextTextCount < dialogues.Length)
        {
            if (dialogues[nextTextCount].Contains("@")) {
                dialogues[nextTextCount] = dialogues[nextTextCount].Replace("@", GameManager.miniGameReselts[miniGame].ToString());
            }
            text.text = dialogues[nextTextCount];
        }            
        NextButton.onClick.RemoveAllListeners();
        if (dialogues.Length == nextTextCount) NextButton.onClick.AddListener(() => gameStart(SceneName.FlappyGameScene));
        else NextButton.onClick.AddListener(() => nextCount());
    }

    public void gameStart(SceneName sceneName)
    {
        GameManager.instance.startFlappyGame(sceneName); //실글톤 바로 호출 좋은건 아니지만 일단 기능 완성부터 
    }
    public void nextCount()
    {
        nextTextCount++;
        UpdateNaxtText();
    }
}
