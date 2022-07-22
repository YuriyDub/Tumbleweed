using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBackGround : MonoBehaviour
{   
    [SerializeField] private float parallaxEffect, movingSpeed;

    private Camera mainCamera;

    private float length, startPositionX, temp, dist;

    private void Start()
    {
        startPositionX = transform.position.x;
        mainCamera = Camera.main;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void FixedUpdate()
    {     
        dist = (mainCamera.transform.position.x * parallaxEffect);
        temp = (mainCamera.transform.position.x * (1 - parallaxEffect));

        if (movingSpeed > 0)
        {
            startPositionX += Time.fixedDeltaTime * movingSpeed;
        }       

        transform.position = new Vector3(startPositionX + dist, transform.position.y, 1);

        if (temp > startPositionX + length)
        {
            startPositionX += length;
        }
        else if (temp < startPositionX - length) 
        {
            startPositionX -= length;
        }
    }
}
