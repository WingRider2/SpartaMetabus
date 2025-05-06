using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    private List<GameObject> NPCPrefabs; // ������ NPC ������ ����Ʈ

    [SerializeField]
    private List<Transform> spawnPos; // NPC ������ ����

    private List<NPCController> activeNPC = new List<NPCController>(); // ���� ������ npc

    public void init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void sponNPC()
    {
        for (int i = 0; i < NPCPrefabs.Count; i++)
        {
            GameObject npc = NPCPrefabs[i];
            Vector2 sponPos = new Vector2(spawnPos[i].position.x, spawnPos[i].position.y);

            GameObject sponNPC = Instantiate(npc,sponPos,Quaternion.identity);
            NPCController npcController = sponNPC.GetComponent<NPCController>();
            npcController.init(this, gameManager.player.transform , gameManager);

            activeNPC.Add(npcController);
        }
    }

}
