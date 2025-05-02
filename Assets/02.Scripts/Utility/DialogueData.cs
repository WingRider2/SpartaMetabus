using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueData
{
    private int dialogueStep = 0; // 대화 단계 
    public string[] dialogues; // 단계별 대화를 저장하는 문자열 배열

    public string GetDialogue() // 대화 반환
    {
        if (dialogues.Length <= dialogueStep)
        {
            return ".....";
        }

        return dialogues[dialogueStep++];
    }

    public void ResetDialogue() // 댜화 단계 초기화
    {
        dialogueStep = 0;
    }

    public bool DialogueComplete() // 대화가 끝났는 지 반환
    {
        return dialogues.Length <= dialogueStep;
    }
}