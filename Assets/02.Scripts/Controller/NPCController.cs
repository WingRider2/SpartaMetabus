using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCController : BaseController
{
    private NPCManager NPCManager;
    private GameManager gameManager;
    private Transform player;

    [SerializeField] private GameObject triggerArea;

    public TextMeshProUGUI text;

    public GameObject button;

    [SerializeField]private string[] lines;
    bool inRange = false;
    public NpcName npcName;
    public MiniGame miniGame;
    [SerializeField] private const float followRange = 15f;

    public void init(NPCManager NPCManager , Transform player , GameManager gameManager)
    {
        this.NPCManager = NPCManager;
        this.player = player;
        this.gameManager = gameManager;
    }
    protected override void Awake()
    {
        base.Awake();
        animationHandler = GetComponent<NPCAnimationHandler>();
        text.text = npcName.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
    }
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.T))//�̰Ŵ� �÷��̾ 1���϶� ������ �κ����� ����
        {
            Debug.Log("����");
            gameManager.openDialogue(lines, miniGame);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            button.SetActive(false);
        }
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        float distance = DistanceToTarget(); //�Ÿ�
        Vector2 direction = DirectionToTarget(); //����

        if(distance <= followRange)
        {
            lookDirection = direction;
        }
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, player.position);//Distance�� �� ������ ���� �Ÿ��� �˷��ִ� ���̴�.
    }
    protected Vector2 DirectionToTarget()
    {
        return (player.position - transform.position).normalized; // Ÿ���� ������ �˷��ش�.
    }

}
