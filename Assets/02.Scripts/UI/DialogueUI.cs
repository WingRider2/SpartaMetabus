using System.Collections;
using System.Collections.Generic;
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

    protected override UIState GetUIState()
    {
        return UIState.Dialogue;
    }
    public void Init(string[] dialogues)
    {
        this.dialogues = null;
        this.dialogues = dialogues;
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
            text.text = dialogues[nextTextCount];
        }            
        NextButton.onClick.RemoveAllListeners();
        if (dialogues.Length == nextTextCount) NextButton.onClick.AddListener(() => gameStart(SceneName.FlappyGameScene));
        else NextButton.onClick.AddListener(() => nextCount());
    }

    public void gameStart(SceneName sceneName)
    {
        GameManager.instance.startFlappyGame(sceneName); //�Ǳ��� �ٷ� ȣ�� ������ �ƴ����� �ϴ� ��� �ϼ����� 
    }
    public void nextCount()
    {
        nextTextCount++;
        UpdateNaxtText();
    }
}
