using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfStackController : MonoBehaviour
{
    private const int dwarfCount = 7; //��� ũ��
    private const float MovingBoundsSize = 3f; //��� �̵�����

    private const float blockHeight = 1.75f;
    private const float blockSpacing = 1.0f;

    private const float StackMovingSpeed = 50.0f; //����� �Ʒ��� �������� �ӵ�
    private const float BlockMovingSpeed = 3.5f; //����̵��ӵ�
    private const float ErrorMargin = 0.1f; //����������
    private bool isGameOver = false;

    public GameObject dwarf = null; //������ ���
    public GameObject dwarfs = null; //������ ����� �θ�
    private Vector3 prevBlockPosition;//����� ��ǥ
    private Vector3 desiredPosition;//���� ��� ��ǥ

    private int nextCount = dwarfCount; // ������� ũ��

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
        Spawn_Block();//ù��ϻ���
    }
    void Update()
    {
        if (isGameOver)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())//������� �������� ������ϻ���
            {
                Spawn_Block();
            }
            else
            {
                // ���� ����
                Debug.Log("GameOver");
                //UpdateScore();
                isGameOver = true;
                //UIManager.Instance.SetScoreUI();
            }
        }
        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime); // ��������� ������ �Ǿ� �ؼ� ��������� �Ʒ���
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

            // ��Ȯ�� ��ġ ���: Y���� stackCount��, X���� spacing
            Vector3 spawnPos = Vector3.right * i * blockSpacing;
            newTrans.localPosition = spawnPos;
            newTrans.localRotation = Quaternion.identity;
        }

        // ��� ���� �� stackCount ���� (���� ��� �غ��)
        stackCount++;

        // ī�޶� �Ʒ��� �̵� ��ġ ����
        desiredPosition = Vector3.down * blockHeight * stackCount;

        
        blockTransition = 0f;
        lastBlock = newPatent.transform;

        return true;
    }
    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        float movePosition = Mathf.PingPong(blockTransition,((float)dwarfCount / 2)) - ((float)dwarfCount / 4);//PingPong�� �� ������ �Դ� ���� �ݺ��ϴ� �Լ��̴�. ���� �ð� , �ڴ� �Ÿ�
        lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, blockHeight * stackCount, secondaryPosition);
    }
    bool PlaceBlock()//����� ������ �ִ��� Ȯ���ϰ� ������ ������ �ΰ� ���κκ� ����
    {
        Vector3 lastPosition = lastBlock.localPosition;//Ŭ���Ѽ��� ��� ��ġ ����

        float deltaX = prevBlockPosition.x - lastPosition.x;// ����ϰ� �����̴� ����� x��ǥ �� �κ���� ����
        bool isNegativeNum = (deltaX < 0) ? true : false; // �ʰ������� �����ΰ� ������� �Ǵ�

        deltaX = Mathf.Abs(deltaX);//���밪

        if (deltaX > ErrorMargin) //©��� �ϴ� �κ��� �ִ°��
        {

            nextCount -= (int)deltaX; //����� ����� ����
            if (nextCount <= 0) //����� ����� 0���� ��ǻ� ��ġ�� �κ��� ������ 
            {
                return false;
            }

            float middle = (prevBlockPosition.x + lastPosition.x) / 2; //�� x�� ��ġ�� ������ ��x�� �߰���             

            Vector3 tempPosition = lastBlock.localPosition; //����� �پ��� �߽�x�� ���� ������ ����ؼ� ������ y,z ��ǥ �ҷ���
            tempPosition.x = middle; //�߰����� ���ο� ��ǥ�� x�� ����
            lastBlock.localPosition = lastPosition = tempPosition; //�����̴� ����� ��ǥ�� ���� �Է�

            float rubbleHalfScale = deltaX / 2f; //������ x��ǥ ���ϱ�

            //CreateRubble(rubbleHalfScale, (int)deltaX);

        }
        else//¥�� �ʿ䰡 ���°��
        {
            lastBlock.localPosition = prevBlockPosition + Vector3.up;
        }


        secondaryPosition = lastBlock.localPosition.x;

        return true;
    }
    void CreateRubble(Vector3 pos, int count) // 
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;// <= �̰����� ���� �����. transform�� ���� �����̶� ������ ���� ���氡���ϴ�.

        go.transform.localPosition = pos; //������ǥ
        go.transform.localRotation = Quaternion.identity; //ȸ�� �⺻��ġ

        go.AddComponent<Rigidbody>(); //Rigidbody�� �߰��ϰ�
        go.name = "Rubble"; //�̸� ����
    }
}
