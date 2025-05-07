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
        if (timeSinceLastChange < healthChangeDelay)//�ǰ� ���ӽð��� ü�º��� �����̺��� ������
        {
            timeSinceLastChange += Time.deltaTime;// �ǰ����ӽ��� ����
            if (timeSinceLastChange >= healthChangeDelay)//������ ü�º�������̺��� Ŀ���� 
            {
                animationHandler.InvincibilityEnd();//�ǰ� ��
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay) //�������� 0�̰ų�, �ǰ����϶� �ǰ� ��������
        {
            return false;
        }

        timeSinceLastChange = 0f; //�ǰ� ���� �ʱ�ȭ
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth; // �ִ�ü�� �ʰ��� �ִ�ü���� ����
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth; // �ʰ����ذ� ������ 0����

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();//�ǰ�
            if (damageClip != null)
                SoundManager.PlayClip(damageClip);
        }

        if (CurrentHealth <= 0f)
        {
            Death();//����
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
