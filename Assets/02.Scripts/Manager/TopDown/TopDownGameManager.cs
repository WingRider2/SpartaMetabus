using UnityEngine;

public class TopDownGameManager : MonoBehaviour
{
    GameManager gameManager;

    public static TopDownGameManager instance;

    private ToDownGameUIManager toDownGameUIManager;
    public static bool isFirstLoading = true;

    private EnemyManager enemyManager; //몬스터 관리자 가져온다
    public PlayerController player { get; private set; } //플레이어 정보
    private ResourceController _playerResourceController; //플레이어 리소스 정보

    [SerializeField] private int currentWaveIndex = 0;

    public void init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        instance = this;//싱글톤생성
        player = FindObjectOfType<PlayerController>();//플레이어 찻고
        //player.Init(gameManager); //플레이어 초기화 

        toDownGameUIManager = FindObjectOfType<ToDownGameUIManager>();

        //_playerResourceController = player.GetComponent<ResourceController>();
        //_playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        //_playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

        enemyManager = GetComponentInChildren<EnemyManager>(); //몬스터 매니저 찾고
        //enemyManager.Init(this); //몬스터 매니저에 게임매니저 정보 전달.
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
    public void StartGame()//게임시작
    {
        toDownGameUIManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave() // 몬스터 웨이브 넘어간다.
    {
        currentWaveIndex += 1;
        toDownGameUIManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void EndOfWave()//웨이브 종료
    {
        StartNextWave();
    }

    public void GameOver() //게임 종료
    {
        enemyManager.StopWave();
        toDownGameUIManager.SetGameOver();
    }
}
