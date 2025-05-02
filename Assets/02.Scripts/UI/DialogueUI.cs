using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : BaseUI
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private Button NextButton;
    protected override UIState GetUIState()
    {
        return UIState.Dialogue;
    }
    void init(string text)
    {
        this.text.text = text;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UpdateNaxtText(int wave)
    {
        text.text = wave.ToString();
    }
}
