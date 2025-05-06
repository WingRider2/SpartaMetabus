using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyGameManager : MonoBehaviour
{
    GameManager gameManager;

    static FlappyGameManager instance;
    public static FlappyGameManager Instance { get { return instance; } }
    UIManager uIManager;
    
    private int currentScore = 0;
    public void init(GameManager gameManager , UIManager uIManager)
    {
        this.gameManager = gameManager;
        instance = this;
        this.uIManager = uIManager;
    }

    private void Start()
    {
        uIManager.FlappyGameUpdateScore(0);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        uIManager.FlappyGameSetRestart();
    }
    public void ReturnTown()
    {
        SceneManager.LoadScene(SceneName.HomeTownScene.ToString());
        gameManager.SetFlappyPoint(currentScore);
        uIManager.ReturnTown();
    }
    public void AddScore(int score)
    {
        currentScore += score;
        uIManager.FlappyGameUpdateScore(currentScore);
    }
}
