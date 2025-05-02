using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NPCController : BaseController
{
    private NPCManager NPCManager;
    private Transform player;

    public TextMeshProUGUI text;

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
        text.text = npc.고블린.ToString();
    }
    protected override void HandleAction()
    {
        base.HandleAction();

        float distance = DistanceToTarget(); //거리
        Vector2 direction = DirectionToTarget(); //방향

        if(distance <= followRange)
        {
            lookDirection = direction;
        }
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, player.position);//Distance는 두 포지션 사이 거리를 알려주는 것이다.
    }
    protected Vector2 DirectionToTarget()
    {
        return (player.position - transform.position).normalized; // 타겟의 방향을 알려준다.
    }

}
