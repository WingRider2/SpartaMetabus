using UnityEngine;

public class TopDownGameManager : MonoBehaviour
{
    GameManager gameManager;

    public static TopDownGameManager instance;

    private ToDownGameUIManager toDownGameUIManager;
    public static bool isFirstLoading = true;

    private EnemyManager enemyManager; //���� ������ �����´�
    public PlayerController player { get; private set; } //�÷��̾� ����
    private ResourceController _playerResourceController; //�÷��̾� ���ҽ� ����

    [SerializeField] private int currentWaveIndex = 0;

    public void init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        instance = this;//�̱������
        player = FindObjectOfType<PlayerController>();//�÷��̾� ����
        //player.Init(gameManager); //�÷��̾� �ʱ�ȭ 

        toDownGameUIManager = FindObjectOfType<ToDownGameUIManager>();

        //_playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        enemyManager = GetComponentInChildren<EnemyManager>(); //���� �Ŵ��� ã��
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
