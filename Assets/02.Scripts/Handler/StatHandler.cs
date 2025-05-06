using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health
    {
        get => health;
        set => health = value;
    }
    [Range(1, 20f)][SerializeField] private float speed = 3;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    [Range(1, 20f)][SerializeField] private float flapForce = 6;
    public float FlapForce
    {
        get => flapForce;
        set => flapForce = value;
    }
}
