using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    private ProjectileManager projectileManager;

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; //����ü ������ġ

    [SerializeField] private int bulletIndex;//����ü��ȣ
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; //����ü ũ��
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//����ü�߻� �����ð�
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;//?
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;//�߻�ü�� ����?
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;//�߻�ü 2�� ������ �߻� ����
    public float MultipleProjectilesAngle { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;//����ü ����
    public Color ProjectileColor { get { return projectileColor; } }

    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngle; //���簢
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; //����ü��

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;//����ü ���� ���� �����ϴ� ����


        for (int i = 0; i < numberOfProjectilesPerShot; i++) //��� ����ü ��ȸ
        {
            float angle = minAngle + projectilesAngleSpace * i; // ���۰������� ���� �Ʒ���
            float randomSpread = Random.Range(-spread, spread); // ������ �����ϰ� ���Ƴ���
            angle += randomSpread; // �߻簢 ���� ��ġ �����ϰ�
            CreateProjectile(Controller.LookDirection, angle); //������ ���� ����
        }
    }
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
