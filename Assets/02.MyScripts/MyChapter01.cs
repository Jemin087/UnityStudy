using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MyChapter01 : MonoBehaviour
{
    [SerializeField]
    private GameObject capsule;

    private float targetAngle = 0f;

    public float capsuleRotationSpeed = 4f;

    private GameObject sphere;
    private float buttonDownTime;


    //X,Y 공이 튕기는 각 조절 
    public float sphereMagnitudeX = 1.0f;
    public float sphereMagnitudeY = 1.0f;
    public float sphereFrequency = 1.0f;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            targetAngle = GetRotationAngleByTargetPosition(Input.mousePosition);

            if (sphere != null)
            {
                Destroy(sphere);
                sphere = null;
            }

            sphere = SpawnSphereAt(Input.mousePosition);
            buttonDownTime = Time.time;

        }
        capsule.transform.eulerAngles 
            = new Vector3(0, 0, Mathf.LerpAngle(capsule.transform.eulerAngles.z, targetAngle, Time.deltaTime * capsuleRotationSpeed));


        if(sphere!=null)
        {
            sphere.transform.position
                = new Vector3(sphere.transform.position.x + (capsule.transform.position.x - sphere.transform.position.x) * Time.deltaTime * sphereMagnitudeX,
                            Mathf.Abs(Mathf.Sin((Time.time - buttonDownTime) * (Mathf.PI) * sphereFrequency) * sphereMagnitudeY), 0);
        }
    }

    float GetRotationAngleByTargetPosition(Vector3 mousePos)
    {
        Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(capsule.transform.position);
        Vector3 diff = mousePos - selfScreenPoint;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float finalAngle = angle - 90f;

        return finalAngle;

    }

    GameObject SpawnSphereAt(Vector3 mousePos)
    {
        GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(capsule.transform.position);
        Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, selfScreenPoint.z));
        sp.transform.position = new Vector3(position.x, position.y,0);
        return sp;
    }
}
