using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlappyUI : BaseUI
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI GameOverText;

    protected override UIState GetUIState()
    {
        return UIState.Flappy;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameOverText == null)
        {
            Debug.LogError("restart text is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }
        GameOverText.gameObject.SetActive(false);
    }

    public void SetRestart()
    {
        GameOverText.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }


}
