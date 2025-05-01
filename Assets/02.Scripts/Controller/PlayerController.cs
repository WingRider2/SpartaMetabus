using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera camera;

    private MoveStategy moveStategy;
    private LookStategy lookStategy;
    public void Init(GameManager gameManager, MoveStategy moveStategy, LookStategy lookStategy)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
        this.moveStategy = moveStategy;
        this.lookStategy = lookStategy;

    }
    void OnMove(InputValue inputValue)
    {
        moveStategy.OnMove(inputValue, out movementDirection);
    }
    void OnLook(InputValue inputValue)
    {
        lookStategy.OnLook(inputValue, transform, camera ,movementDirection, out lookDirection);

    }

    public void SetMoveStrategy(MoveStategy stategy)
    {
        moveStategy = stategy;
    }
    public void SetLookStrategy(LookStategy stategy)
    {
        lookStategy = stategy;
    }
}
