using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    private float offsetX = 0;
    private float offsetY = 0;
    private float offsetZ = -10.0f;

    private float cameraSpeed = 10.0f;
    Vector3 tragetPos;
    private void FixedUpdate()
    {
        Transform targetTransform = Target.transform;
        tragetPos = new Vector3
        (
            targetTransform.position.x + offsetX,
            targetTransform.position.y + offsetY,
            targetTransform.position.z + offsetZ
        );

        transform.position = Vector3.Lerp(transform.position, tragetPos, Time.deltaTime*cameraSpeed);
    }
}
