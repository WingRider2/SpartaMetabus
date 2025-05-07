using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera camera;

    private IMoveStategy moveStategy;
    private ILookStategy lookStategy;
    private IClickStategy clickStategy;
    public void Init(GameManager gameManager, IMoveStategy moveStategy, ILookStategy lookStategy)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
        this.moveStategy = moveStategy;
        this.lookStategy = lookStategy;
    }
    protected override void Awake()
    {
        base.Awake();
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }
    void OnMove(InputValue inputValue)
    {
        moveStategy.OnMove(inputValue, out movementDirection);
    }
    void OnLook(InputValue inputValue)
    {
        lookStategy.OnLook(inputValue, transform, camera ,movementDirection, out lookDirection);
    }
    void OnClick(InputValue inputValue)
    {
        //lookStategy.OnLook(inputValue, transform, camera, movementDirection, out lookDirection);
    }

    void OnFire(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        isAttacking = inputValue.isPressed;
    }

    public void SetMoveStrategy(IMoveStategy stategy)
    {
        moveStategy = stategy;
    }
    public void SetLookStrategy(ILookStategy stategy)
    {
        lookStategy = stategy;
    }
    public override void Death()
    {
        base.Death();
        gameManager.topDownGameManager.GameOver();
    }
}
