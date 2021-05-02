using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Position Controls")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 10f;

    [Tooltip("Horizontally movement range")]
    [SerializeField] float xRange = 10f;
    
    [Tooltip("Vertically movement range")]
    [SerializeField] float yRange = 6.25f;

    [Header("Rotation Controls")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Lasers")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;


    Vector3 lastInputPosition;
    Vector3 lastInputRotation;

    float startTime;
    float timeCount;
    float journeyTime = 1f;

    float xThrow, yThrow;
    bool isControlActive = true;

    public bool IsControlActive { get => isControlActive; set => isControlActive = value; }

    void Update()
    {
        if (isControlActive)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        else 
        {
            ResetTranslation();
            ResetRotation();
        }
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl ;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos;
        float clampedYPos;

        clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            SetLasersActive(true);
        else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space"))
            SetLasersActive(false);
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    private void ResetRotation()
    {
        float fracComplete = (Time.time - startTime) / journeyTime;
        //Debug.Log(transform.localEulerAngles);

        transform.localEulerAngles = Vector3.Slerp(Vector3.zero, Vector3.zero, fracComplete);

        //transform.rotation = Quaternion.Slerp(Quaternion.Euler(lastInputRotation), Quaternion.Euler(Vector3.zero), fracComplete);
        //timeCount += Time.deltaTime;
       
    }

    private void ResetTranslation()
    {
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.localPosition = Vector3.Slerp(lastInputPosition, Vector3.zero, fracComplete);
    }

    public void SetLastInputInformation()
    {
        startTime = Time.time; //last input time

        lastInputPosition = transform.localPosition; //last input time position
        lastInputRotation = transform.localRotation.eulerAngles;//last input time rotation
    }
}
