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

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)//발사위치 , 발사 방향을 받아 , 
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex]; //각총알의 정보를 행들러에서 받아온다.
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);// 발사체 생성

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();//발사체의 컨트롤러를 가져와
        projectileController.Init(direction, rangeWeaponHandler, this);//컨트롤러의 init을 실행한다.
    }
    public void CreateImpactParticlesAtPostion(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position; //파티클 생성위치
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;//파티클 정보 입시저장
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));//발사체 사이즈에 따라 파티클 생성갯수 조정
        ParticleSystem.MainModule mainModule = impactParticleSystem.main; // 파티클 메인 모듈을 불러와서
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f; //파티클 이동속도를 총알 사이츠에 따라 서 설정
        impactParticleSystem.Play(); //수치조정 완료 된 파티클 생성
    }
}