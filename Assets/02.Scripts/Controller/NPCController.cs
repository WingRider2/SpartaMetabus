using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCController : BaseController
{
    private NPCManager NPCManager;
    private Transform player;

    [SerializeField] private const float followRange = 15f;

    public void init(NPCManager NPCManager , Transform player)
    {
        this.NPCManager = NPCManager;
        this.player = player;
    }
    protected override void Awake()
    {
        base.Awake();
        animationHandler = GetComponent<NPCAnimationHandler>();
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
