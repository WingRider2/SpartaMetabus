using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UIManager uiManager;
    private NPCManager npcManager;

    public static bool isFirstLoading = false;

    public PlayerController player { get; private set; }

    private void Awake()
    {
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

}
