using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private UIManager uiManager;
 
    public static bool isFirstLoading = true;

    public PlayerController player { get; private set; }
   
    private void Awake()
    {
        instance = this;//�̱������
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this,new HomeMove(),new DungeonLook());//�ϴ� �����ڷ� ����
        uiManager = FindAnyObjectByType<UIManager>();
    }

}
