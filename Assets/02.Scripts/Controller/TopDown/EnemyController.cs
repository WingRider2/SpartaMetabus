using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    [SerializeField] private float followRange = 15f;

    public void Init(EnemyManager enemyManager, Transform target) //
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (weaponHandler == null || target == null)// 무기가 없거나 대상이 없거나 하면 이동 하지 않는다.
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget(); //거리
        Vector2 direction = DirectionToTarget(); //방향

        isAttacking = false; //공격 안하는중
        if (distance <= followRange) //대상까지 거리가 일정거리 면 
        {
            lookDirection = direction;//보는 방향 변경

            if (distance <= weaponHandler.AttackRange) //다시 대상까지 거리가 사절거리 이내면
            {
                int layerMaskTarget = weaponHandler.target;//다상의 비트를 가져오고
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); //Raycast는 직선으로 보이지 않는 빔을 날려서 빔에 맞은 대상의 정보를 가져온다.

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))//대상이 있으면 , 대상이 공격 가능 대상인지를 확인하여 
                {
                    isAttacking = true;//공격
                }

                movementDirection = Vector2.zero;//공격 예정이기에 멈춘다.
                return;
            }

            movementDirection = direction;//공격을 않하기에 대상을 향해 이동한다.
        }

    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);//Distance는 두 포지션 사이 거리를 알려주는 것이다.
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; // 타겟의 방향을 알려준다.
    }
    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }
}
