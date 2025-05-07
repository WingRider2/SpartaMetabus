using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)//�߻���ġ , �߻� ������ �޾� , 
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex]; //���Ѿ��� ������ ��鷯���� �޾ƿ´�.
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);// �߻�ü ����

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();//�߻�ü�� ��Ʈ�ѷ��� ������
        projectileController.Init(direction, rangeWeaponHandler, this);//��Ʈ�ѷ��� init�� �����Ѵ�.
    }
    public void CreateImpactParticlesAtPostion(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position; //��ƼŬ ������ġ
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;//��ƼŬ ���� �Խ�����
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));//�߻�ü ����� ���� ��ƼŬ �������� ����
        ParticleSystem.MainModule mainModule = impactParticleSystem.main; // ��ƼŬ ���� ����� �ҷ��ͼ�
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f; //��ƼŬ �̵��ӵ��� �Ѿ� �������� ���� �� ����
        impactParticleSystem.Play(); //��ġ���� �Ϸ� �� ��ƼŬ ����
    }
}