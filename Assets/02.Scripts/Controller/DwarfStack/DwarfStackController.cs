using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfStackController : MonoBehaviour
{
    private const int dwarfCount = 7; //블록 크기
    private const float MovingBoundsSize = 3f; //블록 이동범위

    private const float blockHeight = 1.75f;
    private const float blockSpacing = 1.0f;

    private const float StackMovingSpeed = 50.0f; //블록이 아래로 내려가는 속도
    private const float BlockMovingSpeed = 3.5f; //블록이동속도
    private const float ErrorMargin = 0.1f; //허용오차범위
    private bool isGameOver = false;

    public GameObject dwarf = null; //생성할 블록
    public GameObject dwarfs = null; //생성할 블록의 부모
    private Vector3 prevBlockPosition;//전블록 좌표
    private Vector3 desiredPosition;//다음 블록 좌표

    private int nextCount = dwarfCount; // 다음블록 크기

    Transform lastBlock = null;
    float blockTransition = 0f;
    float secondaryPosition = 0f;

    int stackCount = -1;

    void Start()
    {
        if (dwarf == null)
        {
            Debug.Log("OriginBlock is NULL");
            return;
        }
        Spawn_Block();//첫블록생성
    }
    void Update()
    {
        if (isGameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())//전블록이 놓아져서 다음블록생성
            {
                Spawn_Block();
            }
            else
            {
                // 게임 오버
                Debug.Log("GameOver");
                //UpdateScore();
                isGameOver = true;
                //UIManager.Instance.SetScoreUI();
            }
        }
        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime); // 다음블록이 생성이 되야 해서 이전블록을 아래로
    }
    bool Spawn_Block()
    {
        GameObject newPatent = Instantiate(dwarfs);
        newPatent.transform.SetParent(transform,false);
        Vector3 spawnPoss = Vector3.up * blockHeight * stackCount;
        newPatent.transform.localPosition = spawnPoss;

        GameObject newBlock = null;
        Transform newTrans = null;

        for (int i = -nextCount / 2; i <= nextCount / 2; i++)
        {
            newBlock = Instantiate(dwarf);
            if (newBlock == null)
            {
                Debug.Log("NewBlock Instantiate Failed!");
                return false;
            }

            newTrans = newBlock.transform;
            newTrans.SetParent(newPatent.transform, false);

            // 정확한 위치 계산: Y축은 stackCount층, X축은 spacing
            Vector3 spawnPos = Vector3.right * i * blockSpacing;
            newTrans.localPosition = spawnPos;
            newTrans.localRotation = Quaternion.identity;
        }

        // 블록 생성 후 stackCount 증가 (다음 블록 준비용)
        stackCount++;

        // 카메라 아래로 이동 위치 갱신
        desiredPosition = Vector3.down * blockHeight * stackCount;

        
        blockTransition = 0f;
        lastBlock = newPatent.transform;

        return true;
    }
    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        float movePosition = Mathf.PingPong(blockTransition,((float)dwarfCount / 2)) - ((float)dwarfCount / 4);//PingPong은 두 지점을 왔다 갔다 반복하는 함수이다. 앞은 시간 , 뒤는 거리
        lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, blockHeight * stackCount, secondaryPosition);
    }
    bool PlaceBlock()//블록을 놓을수 있는지 확인하고 놓을수 있을때 부가 적인부분 삭제
    {
        Vector3 lastPosition = lastBlock.localPosition;//클릭한순간 블록 위치 저장

        float deltaX = prevBlockPosition.x - lastPosition.x;// 전블록과 웅직이던 블록의 x죄표 비교 두블록의 차이
        bool isNegativeNum = (deltaX < 0) ? true : false; // 초과범위가 음수인가 양수인지 판단

        deltaX = Mathf.Abs(deltaX);//절대값

        if (deltaX > ErrorMargin) //짤라야 하는 부분이 있는경우
        {

            nextCount -= (int)deltaX; //블록의 사이즈를 감소
            if (nextCount <= 0) //블록의 사이즈가 0이하 사실상 겹치는 부분이 없으면 
            {
                return false;
            }

            float middle = (prevBlockPosition.x + lastPosition.x) / 2; //두 x를 합치고 나누면 두x의 중간값             

            Vector3 tempPosition = lastBlock.localPosition; //사이즈가 줄어들어 중심x의 값에 변경을 줘야해서 나무저 y,z 죄표 불러옴
            tempPosition.x = middle; //중간값을 새로운 좌표의 x에 저장
            lastBlock.localPosition = lastPosition = tempPosition; //웅직이던 블록의 좌표를 새로 입력

            float rubbleHalfScale = deltaX / 2f; //러블의 x죄표 구하기

            //CreateRubble(rubbleHalfScale, (int)deltaX);

        }
        else//짜를 필요가 없는경우
        {
            lastBlock.localPosition = prevBlockPosition + Vector3.up;
        }


        secondaryPosition = lastBlock.localPosition.x;

        return true;
    }
    void CreateRubble(Vector3 pos, int count) // 
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;// <= 이개념이 조금 힘들다. transform이 참조 개념이라 생성후 값을 변경가능하다.

        go.transform.localPosition = pos; //생성죄표
        go.transform.localRotation = Quaternion.identity; //회전 기본수치

        go.AddComponent<Rigidbody>(); //Rigidbody를 추가하고
        go.name = "Rubble"; //이름 짓지
    }
}
