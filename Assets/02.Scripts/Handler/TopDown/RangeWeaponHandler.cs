using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{
    private ProjectileManager projectileManager;

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition; //투사체 생성위치

    [SerializeField] private int bulletIndex;//투사체번호
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1; //투사체 크기
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;//투사체발사 지연시간
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;//?
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;//발사체당 개수?
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;//발사체 2개 사이의 발사 각도
    public float MultipleProjectilesAngle { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;//투사체 색상
    public Color ProjectileColor { get { return projectileColor; } }

    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngle; //투사각
        int numberOfProjectilesPerShot = numberofProjectilesPerShot; //투사체수

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;//투사체 수에 따라 시작하는 각도


        for (int i = 0; i < numberOfProjectilesPerShot; i++) //모든 투사체 순회
        {
            float angle = minAngle + projectilesAngleSpace * i; // 시작각도에서 점점 아래로
            float randomSpread = Random.Range(-spread, spread); // 범위를 랜덤하게 봅아낸다
            angle += randomSpread; // 발사각 각도 수치 랜덤하게
            CreateProjectile(Controller.LookDirection, angle); //각도에 따라 생성
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
