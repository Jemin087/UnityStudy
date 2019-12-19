using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   
    [SerializeField]
    Transform targetObject;

    float dist = 5f;

    float xSpeed = 220.0f;
    float ySpeed = 100.0f;

    float cameraX = 0f;
    float cameraY = 0f;

    float minCameraY = -20f;
    float maxCameraY = 70f;

    float ClampAngle(float angle,float min, float max)
    {
        if(angle<-360)
        {
            angle += 360;
        }
        if(angle>360)
        {
            angle -= 360;       
        }

        return Mathf.Clamp(angle, min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;

        cameraX = angles.x;
        cameraY = angles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraX += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        cameraY -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        cameraY = ClampAngle(cameraY, minCameraY, maxCameraY);

        Quaternion rotation = Quaternion.Euler(cameraY, cameraX, 0);
        Vector3 pos = rotation * new Vector3(0, 0.9f, -dist) + targetObject.position + new Vector3(0f, 0f, 0f);
        transform.rotation = rotation;
        targetObject.rotation = Quaternion.Euler(0, cameraX, 0);
        transform.position = pos;
    }
}
