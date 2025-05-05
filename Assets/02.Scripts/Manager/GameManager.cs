using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    private NPCManager npcManager;

    public static bool isFirstLoading = false;
    public PlayerController player { get; private set; }

    public Dictionary<miniGame, int> miniGameReselts = new();
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;//�̱������
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this,new HomeMove(),new DungeonLook());//�ϴ� �����ڷ� ����
        uiManager = FindAnyObjectByType<UIManager>();

        npcManager = GetComponentInChildren<NPCManager>();
        npcManager.init(this);
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
}
