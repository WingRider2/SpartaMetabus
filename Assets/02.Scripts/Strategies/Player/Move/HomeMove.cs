using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HomeMove : IMoveStategy
{

    public void OnMove(InputValue inputValue , out Vector2 movementDirection)
    {
        //�Ŀ� ������ �߰� �ϸ� ���콺�� �̵� ����
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
}
