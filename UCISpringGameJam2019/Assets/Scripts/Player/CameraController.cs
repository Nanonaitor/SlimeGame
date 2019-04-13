using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float cameraDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private float zoomRate;
    [SerializeField] private float cameraHeight;
    [SerializeField] private float xAngle;

    private float minXAngle = 0.0f;
    private float maxYAngle = 90.0f;
    private float yEuler = 0.0f;


    float mouseWheelInput
    {
        get
        {
            float input = Input.GetAxis("Mouse ScrollWheel");
            return input;
        }
    }

    void Start()
    {
        transform.position = target.position;
        Vector3 angles = transform.eulerAngles;
        yEuler = angles.y;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Zoom();
        SetCamera();
    }

    void Zoom()
    {
        if (mouseWheelInput > 0)
        {
            cameraDistance -= Time.deltaTime * zoomRate;

            if (cameraDistance < minDistance)
            {
                cameraDistance = minDistance;
            }
        }
        else
            if (mouseWheelInput < 0)
        {
            cameraDistance += Time.deltaTime * zoomRate;

            if (cameraDistance > maxDistance)
            {
                cameraDistance = maxDistance;
            }
        }
    }

    void SetCamera()
    {
        Quaternion newRotation = Quaternion.Euler(xAngle, yEuler, 0);
        transform.rotation = newRotation;

        Vector3 pivot = newRotation * Vector3.forward * cameraDistance + new Vector3(0, -1 * cameraHeight, 0);
        Vector3 newPos = target.position - pivot;
        transform.position = newPos;
    }
}

