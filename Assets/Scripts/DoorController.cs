using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private WheelController wheelController;
    [SerializeField] private Animator lampAnimator;

    [Header("Parameters")]
    [Range(-360, 360)] [SerializeField] private float angleTo;

    private bool hasActivated;

    private float startRotation;

    private float percent;
    private void Start()
    {
        startRotation = transform.rotation.eulerAngles.z;
    }
    private void Update()
    {
        InitializingFields();
    }
    private void FixedUpdate()
    {
        RotateToAngle(hasActivated, angleTo, percent);
    }
    private void RotateToAngle(bool hasActivated ,float angleTo,float percent)
    {
        transform.localRotation = Quaternion.Euler(0, 0, angleTo * percent / -100 + startRotation);

        if (hasActivated) lampAnimator.SetBool("isShining", true);
        else lampAnimator.SetBool("isShining", false);
    }
    private void InitializingFields()
    {
        hasActivated = wheelController.hasActivated;
        percent = wheelController.percent;
    }
}
