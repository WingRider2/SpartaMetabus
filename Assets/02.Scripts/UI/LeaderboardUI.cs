using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Button OpenButton;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panels;
    [SerializeField] private GameObject panelPrefab;
    private TextMeshProUGUI panelPrefabText;


    private float length = -30;

    [SerializeField] private bool isPanelOpen = false;
    public void openPanel()
    {
        if (!isPanelOpen) openPannels(GameManager.miniGameReselts);
        else if (isPanelOpen) closePannels(panels.transform);
    }
    public void openPannels(Dictionary<MiniGame, int> miniGameReselts)
    {
        int count = 0;
        foreach (var item in miniGameReselts)
        {
            Vector2 sponpos = new Vector2(0, length * count++);
            GameObject go = Instantiate(panelPrefab);
            go.transform.SetParent(panels.transform,false);
            go.transform.localPosition = sponpos;
            panelPrefabText = go.GetComponentInChildren<TextMeshProUGUI>();
            panelPrefabText.text = $"{item.Key.ToString()} {item.Value.ToString()}";
        }
        isPanelOpen = true;
    }
    public void closePannels(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        isPanelOpen =false;
    }
}
