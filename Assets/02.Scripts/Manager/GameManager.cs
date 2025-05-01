using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isFirstLoading = true;

    public PlayerController player { get; private set; }
   
    private void Awake()
    {
        instance = this;//½Ì±ÛÅæ»ý¼º
        player = FindAnyObjectByType<PlayerController>();
        player.Init(this);
    }

}
