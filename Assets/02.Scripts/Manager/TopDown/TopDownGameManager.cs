using UnityEngine;

public class TopDownGameManager : MonoBehaviour
{
    GameManager gameManager;
    UIManager uIManager;
    public static TopDownGameManager instance;

    private ToDownGameUIManager toDownGameUIManager;
    public static bool isFirstLoading = true;

    private EnemyManager enemyManager; //���� ������ �����´�
    public PlayerController player { get; private set; } //�÷��̾� ����
    private ResourceController _playerResourceController; //�÷��̾� ���ҽ� ����

    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private GameObject playerPrefab;

    public void init(GameManager gameManager, UIManager uIManager)
    {
        this.gameManager = gameManager;
        instance = this;
        this.uIManager = uIManager;

        GameObject playerObj = Instantiate(playerPrefab);
        player = playerObj.GetComponent<PlayerController>();
        player.Init(gameManager, new DungeonMove(), new DungeonLookStategy()); //�÷��̾� �ʱ�ȭ 
    }

    private void Awake()
    {
        instance = this;//�̱������       
        

        toDownGameUIManager = FindObjectOfType<ToDownGameUIManager>();

        //_playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        enemyManager = GetComponentInChildren<EnemyManager>(); //���� �Ŵ��� ã��
        enemyManager.Init(gameManager);
        //enemyManager.Init(this); //���� �Ŵ����� ���ӸŴ��� ���� ����.
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
    public void StartGame()//���ӽ���
    {
        toDownGameUIManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave() // ���� ���̺� �Ѿ��.
    {
        currentWaveIndex += 1;
        toDownGameUIManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()//���̺� ����
    {
        StartNextWave();
    }

    public void GameOver() //���� ����
    {
        enemyManager.StopWave();
        toDownGameUIManager.SetGameOver();
    }
}
