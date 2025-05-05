using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    private NPCManager npcManager;
    private FlappyGameManager flappyGameManager;

    public static bool isFirstLoading = false;
    public PlayerController player { get; private set; }

    public Dictionary<miniGame, int> miniGameReselts = new();

    private void Awake()
    {
        var go = GameObject.Find("GameManager");
        DontDestroyOnLoad(this.gameObject);
        instance = this;//싱글톤생성
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this,new HomeMove(),new DungeonLook());//일단 생성자로 구현
        uiManager = FindAnyObjectByType<UIManager>();

        npcManager = GetComponentInChildren<NPCManager>();
        npcManager.init(this);

        flappyGameManager = GetComponentInChildren<FlappyGameManager>(true);        
    }
    private void Start()
    {
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }
    public void StartGame()
    {
        npcManager.sponNPC();
    }
    public void openDialogue(string[] Dialogues)
    {
        uiManager.SetOndialogue(Dialogues);
    }

    public void startFlappyGame(SceneName sceneName)
    {
        SceneManager.LoadScene($"{sceneName}");
        flappyGameManager.transform.gameObject.SetActive(true);
        uiManager.SetPlayFlappyGame();
        flappyGameManager.init(this , uiManager);
    }
}
