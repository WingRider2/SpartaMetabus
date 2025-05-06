using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HomeMove : IMoveStategy
{

    public void OnMove(InputValue inputValue , out Vector2 movementDirection)
    {
        //후에 길찻기 추가 하면 마우스로 이동 구현
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
}
