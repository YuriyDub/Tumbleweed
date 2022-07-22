using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestoy : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    void Start()
    {
        Invoke("DestoyObj", timeToDestroy);
    }
    private void DestoyObj()
    {
        Destroy(gameObject);
    }
}
