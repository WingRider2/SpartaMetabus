using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private Transform weaponPivot;

    protected Rigidbody2D _rigidbody;
    protected IAnimationHandler animationHandler;
    protected StatHandler statHandler;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }



    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();        
        statHandler = GetComponent<StatHandler>();
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        
    }
    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {

    }

    private void Rotate(Vector2 direction)// 무기회전
    {
        float rotZ = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        bool isLeft = direction.x < 0f;

        if (Renderer.flipX != isLeft)
            Renderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 무기 방향을 돌려준다.
        }
    }

    private void FlipX(Vector2 direction)
    {

    }
    private void Movment(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }
}
