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

        if (weaponHandler == null || target == null)// ���Ⱑ ���ų� ����� ���ų� �ϸ� �̵� ���� �ʴ´�.
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget(); //�Ÿ�
        Vector2 direction = DirectionToTarget(); //����

        isAttacking = false; //���� ���ϴ���
        if (distance <= followRange) //������ �Ÿ��� �����Ÿ� �� 
        {
            lookDirection = direction;//���� ���� ����

            if (distance <= weaponHandler.AttackRange) //�ٽ� ������ �Ÿ��� �����Ÿ� �̳���
            {
                int layerMaskTarget = weaponHandler.target;//�ٻ��� ��Ʈ�� ��������
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget); //Raycast�� �������� ������ �ʴ� ���� ������ ���� ���� ����� ������ �����´�.

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))//����� ������ , ����� ���� ���� ��������� Ȯ���Ͽ� 
                {
                    isAttacking = true;//����
                }

                movementDirection = Vector2.zero;//���� �����̱⿡ �����.
                return;
            }

            movementDirection = direction;//������ ���ϱ⿡ ����� ���� �̵��Ѵ�.
        }

    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);//Distance�� �� ������ ���� �Ÿ��� �˷��ִ� ���̴�.
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; // Ÿ���� ������ �˷��ش�.
    }
    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }
}
