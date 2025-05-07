using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;//무기 정보 
    private ProjectileManager projectileManager;

    private float currentDuration;//대기시간?
    private Vector2 direction; //방향
    private bool isReady; //발사준비 확인
    private Transform pivot; //발사위치

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }
        //속도 설정
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) //충돌 처리
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))//비트연산자 사용하는이유는 layer정보는 비트로 저장을 한다.
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockback)//? 넉백중이라면?
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }


    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler , ProjectileManager projectileManager) //발사체 정보 초기화용
    {
        this.projectileManager = projectileManager;

        rangeWeaponHandler = weaponHandler;//핸들러 정보를 받아

        this.direction = direction;//방향정보 저장
        currentDuration = 0; //발사 대기시간 초기화
        transform.localScale = Vector3.one * weaponHandler.BulletSize;//발사체 크기 조정 기본사이즈one
        spriteRenderer.color = weaponHandler.ProjectileColor;//발사체 색상조정

        transform.right = this.direction;

        if (this.direction.x < 0) //발사 방향에 따른 투사체의 방향 조정
            pivot.localRotation = Quaternion.Euler(180, 0, 0); 
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;//발사 준비 완료
    }

    private void DestroyProjectile(Vector3 position, bool createFx)//발사체 파괴에 사용하는 함수 근데 createFx충돌 효과용? 는 아직 사용하지 않음position도 사용 않하는중입
    {
        if (createFx)//들어오면 파티클이팩트 생성
        {
            projectileManager.CreateImpactParticlesAtPostion(position, rangeWeaponHandler);
        }
        Destroy(this.gameObject);
    }
}
