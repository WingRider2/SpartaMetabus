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

    public static Dictionary<MiniGame, int> miniGameReselts;

    private void Awake()
    {
        var go = GameObject.Find("GameManager");
        DontDestroyOnLoad(this.gameObject);
        instance = this;//싱글톤생성
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this,new HomeMove(),new HomeLookStategy());//일단 생성자로 구현
        uiManager = FindAnyObjectByType<UIManager>();
        uiManager.init(this);
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
        if (miniGameReselts == null)
        {
            miniGameReselts=new Dictionary<MiniGame, int>();
        }
        if (!miniGameReselts.ContainsKey(MiniGame.Flappy))
        {            
            miniGameReselts.Add(MiniGame.Flappy, 0);
        }
        if (!miniGameReselts.ContainsKey(MiniGame.Stack))
        {
            miniGameReselts.Add(MiniGame.Stack, 0);
        }
        if (!miniGameReselts.ContainsKey(MiniGame.TopDown))
        {
            miniGameReselts.Add(MiniGame.TopDown, 0);
        }
    }
    public void StartGame()
    {
        npcManager.sponNPC();
    }
    public void openDialogue(string[] Dialogues, MiniGame miniGame)
    {
        uiManager.SetOndialogue(Dialogues, miniGame);
    }
    public void closeDialogue()
    {
        uiManager.SetoOffdialogue();
    }
    public void SetFlappyPoint(int point)
    {
        Debug.Log(point);
        miniGameReselts[MiniGame.Flappy] = point;
    }
    public void startGame(MiniGame minigame)
    {
        if(minigame == MiniGame.Flappy)
        {
            startFlappyGame(SceneName.FlappyGameScene);
        }
        else if(minigame == MiniGame.Stack)
        {
            startStackGame(SceneName.DwarfStackGameScene);
        }
    }
    public void startFlappyGame(SceneName sceneName)
    {
        SceneManager.LoadScene($"{sceneName}");
        flappyGameManager.transform.gameObject.SetActive(true);
        uiManager.SetPlayFlappyGame();//추후 미니게임 추가시 변경
        flappyGameManager.init(this , uiManager);
    }
    public void startStackGame(SceneName sceneName)
    {
        SceneManager.LoadScene($"{sceneName}");
        //flappyGameManager.transform.gameObject.SetActive(true);
        //uiManager.SetPlayFlappyGame();//추후 미니게임 추가시 변경
        //flappyGameManager.init(this, uiManager);
    }
}
