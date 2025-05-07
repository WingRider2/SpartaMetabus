using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;
    public AudioClip damageClip;

    private float timeSinceLastChange = float.MaxValue;
    [SerializeField] public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    private Action<float, float> OnChangeHealth;
    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)//피격 지속시간이 체력변경 딜레이보다 작으면
        {
            timeSinceLastChange += Time.deltaTime;// 피격지속시장 증가
            if (timeSinceLastChange >= healthChangeDelay)//증가후 체력변경딜레이보가 커지면 
            {
                animationHandler.InvincibilityEnd();//피격 끝
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay) //데미지가 0이거나, 피격중일땐 피격 받지않음
        {
            return false;
        }

        timeSinceLastChange = 0f; //피격 시작 초기화
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; // 최대체력 초과시 최대체력을 리턴
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth; // 초과피해가 들어오면 0으로

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();//피격
            if (damageClip != null)
                SoundManager.PlayClip(damageClip);
        }

        if (CurrentHealth <= 0f)
        {
            Death();//죽음
        }

        return true;
    }
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
    private void Death()
    {
        baseController.Death();
    }
}
