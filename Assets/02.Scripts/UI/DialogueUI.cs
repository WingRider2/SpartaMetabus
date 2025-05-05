using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogueUI : BaseUI
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Button NextButton;

    string[] dialogues = null;
    int nextTextCount = 0;

    protected override UIState GetUIState()
    {
        return UIState.Dialogue;
    }
    public void Init(string[] dialogues)
    {
        this.dialogues = dialogues;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateNaxtText()
    {
        text.text = dialogues[nextTextCount];
        NextButton.onClick.RemoveAllListeners();
        if (dialogues.Length == nextTextCount) NextButton.onClick.AddListener(() => gameStart(SceneName.FlappyGame));
        else NextButton.onClick.AddListener(() => nextCount());
    }

    public void gameStart(SceneName sceneName)
    {
        SceneManager.LoadScene($"{sceneName}", LoadSceneMode.Additive);
    }
    public void nextCount()
    {
        nextTextCount++;
    }
}
