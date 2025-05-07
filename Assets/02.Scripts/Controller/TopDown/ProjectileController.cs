using UnityEngine;


public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;//���� ���� 
    private ProjectileManager projectileManager;

    private float currentDuration;//���ð�?
    private Vector2 direction; //����
    private bool isReady; //�߻��غ� Ȯ��
    private Transform pivot; //�߻���ġ

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
        //�ӵ� ����
        _rigidbody.velocity = direction * rangeWeaponHandler.Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) //�浹 ó��
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))//��Ʈ������ ����ϴ������� layer������ ��Ʈ�� ������ �Ѵ�.
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockback)//? �˹����̶��?
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


    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler , ProjectileManager projectileManager) //�߻�ü ���� �ʱ�ȭ��
    {
        this.projectileManager = projectileManager;

        rangeWeaponHandler = weaponHandler;//�ڵ鷯 ������ �޾�

        this.direction = direction;//�������� ����
        currentDuration = 0; //�߻� ���ð� �ʱ�ȭ
        transform.localScale = Vector3.one * weaponHandler.BulletSize;//�߻�ü ũ�� ���� �⺻������one
        spriteRenderer.color = weaponHandler.ProjectileColor;//�߻�ü ��������

        transform.right = this.direction;

        if (this.direction.x < 0) //�߻� ���⿡ ���� ����ü�� ���� ����
            pivot.localRotation = Quaternion.Euler(180, 0, 0); 
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;//�߻� �غ� �Ϸ�
    }

    private void DestroyProjectile(Vector3 position, bool createFx)//�߻�ü �ı��� ����ϴ� �Լ� �ٵ� createFx�浹 ȿ����? �� ���� ������� ����position�� ��� ���ϴ�����
    {
        if (createFx)//������ ��ƼŬ����Ʈ ����
        {
            projectileManager.CreateImpactParticlesAtPostion(position, rangeWeaponHandler);
        }
        Destroy(this.gameObject);
    }
}
