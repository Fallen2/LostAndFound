using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [Header("Transform references")]
    public Transform headTransform;
    public Transform cameraTransform;

    [Header("Head bobbing")]
    public float bobFrequency = 5f;
    public float bobHorizontalAmplitude = 0.1f;
    public float bobVerticalAmplitude = 0.1f;
    [Range(0f, 1f)] public float headBobSmoothing = 0.1f;

    //State
    public bool isWalking;
    private float walkingTime;
    private Vector3 targetCameraPosition;

    public Animator animator;

    private void Update()
    {
        if(animator.GetBool("Is Walking") || animator.GetBool("Is Running"))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //Set time and offset to 0
        if(!isWalking)
        {
            walkingTime = 0f;
        }
        else
        {
            walkingTime += Time.deltaTime;
        }

        //Calculate the camera target position
        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(walkingTime);

        //Interpolate position
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);
        //Snap to position if it is close enough
        if((cameraTransform.position - targetCameraPosition).magnitude <= 0.001f)
        {
            cameraTransform.position = targetCameraPosition;
        }
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0f;
        float verticalOffset = 0f;
        Vector3 offset = Vector3.zero;

        if(t > 0f)
        {
            //Calculate offsets
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmplitude;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2f) * bobVerticalAmplitude;

            //Combine offsets and relative to the head's position and calculate the camera's target position
            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }

        return offset;
    }
}
