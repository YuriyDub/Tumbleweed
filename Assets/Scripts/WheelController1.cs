using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private Animator lampAnimator;

    public bool hasActivated;

    public float percent;

    private void Update()
    {
     

        if (hasActivated) lampAnimator.SetBool("isShining", true);
        else lampAnimator.SetBool("isShining", false);
    }

    private void PressButton()
    {

    }
   
}
