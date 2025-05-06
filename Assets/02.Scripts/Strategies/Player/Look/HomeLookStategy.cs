using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class HomeLookStategy : ILookStategy
{
    public void OnLook(InputValue inputValue, Transform transform, Camera camera, Vector2 movementDirection, out Vector2 lookDirection)
    {
        lookDirection = movementDirection;
    }
}
