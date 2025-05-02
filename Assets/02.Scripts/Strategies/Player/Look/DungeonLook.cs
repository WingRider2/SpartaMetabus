using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DungeonLook : LookStategy
{
    public void OnLook(InputValue inputValue, Transform transform, Camera camera , Vector2 movementDirection, out Vector2 lookDirection)
    {
        
        Vector2 moserPos = inputValue.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(moserPos);
        lookDirection = (worldPos - (Vector2)transform.position);

        if(lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection;
        }
    }
}
