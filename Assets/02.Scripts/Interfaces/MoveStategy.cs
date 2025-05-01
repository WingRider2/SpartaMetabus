using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem;

public interface MoveStategy
{
    void OnMove(InputValue inputValue, out Vector2 movementDirection);
}
