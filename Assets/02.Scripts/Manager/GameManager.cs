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
        instance = this;//教臂沛积己
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this,new HomeMove(),new DungeonLook());//老窜 积己磊肺 备泅
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
