using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    Transform targetTr;

    public float dist = 4f;

    public float xSpeed = 220f;
    public float ySpeed = 100f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float x = 0f;
    private float y = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
    }

    float ClampAngle(float angle,float min,float max)
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

    // Update is called once per frame
    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + targetTr.position + new Vector3(0, 0, 0);

        transform.rotation = rotation;
        targetTr.rotation = Quaternion.Euler(0, x, 0);
        transform.position = position;
    }
}
