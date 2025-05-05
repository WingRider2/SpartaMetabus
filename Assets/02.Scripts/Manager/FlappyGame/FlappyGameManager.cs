using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyGameManager : MonoBehaviour
{
    static FlappyGameManager instance;
    public static FlappyGameManager Instance { get { return instance; } }
    static FlappyUIManager flappyUiManager;
    public static FlappyUIManager FlappyUiManager { get { return flappyUiManager; } }

    private int currentScore = 0;

    private void Awake()
    {
        instance = this;
        flappyUiManager = FindObjectOfType<FlappyUIManager>();
    }
    private void Start()
    {
        flappyUiManager.UpdateScore(0);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        flappyUiManager.SetRestart();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void AddScore(int score)
    {
        currentScore += score;
        flappyUiManager.UpdateScore(currentScore);
    }
}
