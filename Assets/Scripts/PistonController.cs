using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonController : MonoBehaviour
{
    [SerializeField] private WheelController wheelController;
    [SerializeField] private GameObject pusher;
    [SerializeField] private Animator lampAnimator;

    [SerializeField] private bool hasDowncast = true;

    private Transform pusherTransform;

    Vector3 startTransformPosition;

    private bool hasActivated;

    private float length;
    private float percent;

    void Start()
    {
        Vector3 size = pusher.GetComponent<SpriteRenderer>().bounds.size;
        pusherTransform = pusher.GetComponent<Transform>();

        length = Mathf.Sqrt(Mathf.Pow(size.x,2f) + Mathf.Pow(size.y,2f));

        startTransformPosition = pusherTransform.localPosition;
    }
    private void Update()
    {
        InitializingFields();
    }
    private void FixedUpdate()
    {
        MovePusher(percent, hasActivated, startTransformPosition);
    }
    private void MovePusher(float percent, bool hasActivated, Vector3 startTransformPosition)
    {
        if (hasDowncast) 
        {
            pusherTransform.localPosition = startTransformPosition + new Vector3(0, length * percent / 100, 0);
        }
        else
        {
            pusherTransform.localPosition = startTransformPosition - new Vector3(0, (length * percent / 100) - length, 0);
        }

        if (hasActivated) lampAnimator.SetBool("isShining", true);
        else lampAnimator.SetBool("isShining", false);
    }
    private void InitializingFields()
    {
        hasActivated = ButtonController.hasActivated;
        percent = ButtonController.percent;
    }
}
