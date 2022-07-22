using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBird : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform firstPointTransform, secondPointTransform;

    [Header("Parameters")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed,height;
    [SerializeField] private float interpolationRatio = 0;

    private void FixedUpdate()
    {
        Fly(firstPointTransform.position, secondPointTransform.position);
    }
    void Fly(Vector3 firstPoint, Vector3 secondPoint)
    {
        if (transform.position.x != secondPoint.x)
        {
            transform.position = new Vector3(Vector3.Lerp(firstPoint, secondPoint, interpolationRatio).x, cameraTransform.position.y + height, 1);
            interpolationRatio += Time.fixedDeltaTime * speed * 0.001f;
        }
        else
        {
            interpolationRatio = 0;

            if (transform.position.x != firstPoint.x)
            {
                transform.position = new Vector3(Vector3.Lerp(firstPoint, secondPoint, interpolationRatio).x, cameraTransform.position.y + height, 1);
                interpolationRatio -= Time.deltaTime * speed * 0.001f;
            }
            else
            {
                interpolationRatio = 0;
            }
        }

    }
}
