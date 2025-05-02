using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTriggerController : MonoBehaviour
{
    public GameObject button;

    public GameManager DialogueUI;

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) button.SetActive(false);
    }
}