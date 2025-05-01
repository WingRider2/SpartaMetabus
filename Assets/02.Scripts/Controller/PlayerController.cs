using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera camera;
    protected void init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;

    }
    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
    void onLook(InputValue inputValue)
    {
        Vector2 moserPos = inputValue.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(moserPos);
        lookDirection = (worldPos - (Vector2)transform.position);

        if(lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}
