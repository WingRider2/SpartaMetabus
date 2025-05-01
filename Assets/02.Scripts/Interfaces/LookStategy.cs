using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface LookStategy
{
    void OnLook(InputValue inputValue,Transform transform ,Camera camera,Vector2 movementDirection, out Vector2 lookDirection);
}
