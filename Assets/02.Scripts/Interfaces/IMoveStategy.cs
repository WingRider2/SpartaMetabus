using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.InputSystem;

public interface IMoveStategy
{
    void OnMove(InputValue inputValue, out Vector2 movementDirection);
}
