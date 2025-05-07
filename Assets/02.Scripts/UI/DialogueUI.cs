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
    [SerializeField] private Button CancelButton;
    [SerializeField] private TextMeshProUGUI buttoText;
    [SerializeField] string[] dialogues = null;
    [SerializeField] int nextTextCount = 1;
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
        CancelButton.onClick.AddListener(() => recertCount(0)); //거절 버튼
    }

    public void UpdateNaxtText()
    {
        if (nextTextCount < dialogues.Length)
        {
            if (dialogues[nextTextCount].Contains("@"))
            {
                dialogues[nextTextCount] = dialogues[nextTextCount].Replace("@", GameManager.miniGameReselts[miniGame].ToString());
            }
            text.text = dialogues[nextTextCount];
        }
        NextButton.onClick.RemoveAllListeners();
        if (dialogues.Length == nextTextCount) NextButton.onClick.AddListener(() => gameStart(miniGame));
        else NextButton.onClick.AddListener(() => nextCount());
    }
    public void testUpdateNaxtText()
    {

        NextButton.onClick.RemoveAllListeners();

        if (nextTextCount < dialogues.Length)
        {

            if (dialogues[nextTextCount].Contains("@"))
            {
                dialogues[nextTextCount] = dialogues[nextTextCount].Replace("@", GameManager.miniGameReselts[miniGame].ToString());
            }

            text.text = dialogues[nextTextCount];
        }

        if (dialogues.Length == nextTextCount) NextButton.onClick.AddListener(() => gameStart(miniGame));
        else NextButton.onClick.AddListener(() => nextCount());
    }
    public void gameStart(MiniGame MiniGame)
    {
        GameManager.instance.startGame(miniGame); //실글톤 바로 호출 좋은건 아니지만 일단 기능 완성부터 
    }
    public void nextCount()
    {
        nextTextCount++;
        UpdateNaxtText();
    }
    public void recertCount(int num)
    {
        nextTextCount = num;
        UpdateNaxtText();
    }
}
