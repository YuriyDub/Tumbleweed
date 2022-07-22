using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WheelController : MonoBehaviour
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private Animator lampAnimator;

    public bool hasActivated;

    public float angle;
    public float percent;
    private void Update()
    {
        percent = Percent(wheelTransform.rotation.eulerAngles.z, 360f, ref hasActivated);
        angle = wheelTransform.rotation.eulerAngles.z;

        if (hasActivated) lampAnimator.SetBool("isShining", true);
        else lampAnimator.SetBool("isShining", false);
    }
    private float Percent(float inputValue, float maxValue, ref bool hasActivated)
    {

        if (inputValue / maxValue * 100f > 90f)
        {
            hasActivated = true;
        }
        else
        {
            hasActivated = false;

        }

        return Mathf.Abs(inputValue) / maxValue * 100;
    }
}
