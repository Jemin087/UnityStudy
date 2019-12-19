using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MyChapter01 : MonoBehaviour
{

    /*Chapter01 
     */
    [SerializeField]
    private GameObject capsule;

    private float targetAngle = 0f;

    public float capsuleRotationSpeed = 4f;

    private GameObject sphere;
    private float buttonDownTime;


    //X,Y 공이 튕기는 각 조절 
     
    public float sphereMagnitudeX = 1.0f;
    public float sphereMagnitudeY = 4.0f;
    //공이 튕겨지는 속도 조절
    public float sphereFrequency = 1.0f;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            //클릭한 좌표에서  Angle을 가져온다.
            targetAngle = GetRotationAngleByTargetPosition(Input.mousePosition);
            
            if (sphere != null)
            {
                //마우스 버튼을 눌렀을때 이미 공이 있다면 지우고 초기화
                Destroy(sphere);
                sphere = null;
            }
            //SpawnSphereAt 함수로 sphere를 MousePos 위치에 생성
            sphere = SpawnSphereAt(Input.mousePosition);
            buttonDownTime = Time.time;
            //Debug.Log("buttonDownTime : " + buttonDownTime);
        }
        capsule.transform.eulerAngles 
            = new Vector3(0, 0, Mathf.LerpAngle(capsule.transform.eulerAngles.z, targetAngle, Time.deltaTime * capsuleRotationSpeed));

       
        if(sphere!=null)
        {
            //원래코드
            sphere.transform.position
                = new Vector3(sphere.transform.position.x + (capsule.transform.position.x - sphere.transform.position.x) * Time.deltaTime * sphereMagnitudeX,
                            Mathf.Abs((Mathf.Sin(Time.time - buttonDownTime) * (Mathf.PI) * sphereFrequency) * sphereMagnitudeY), 0);

            //sphere.transform.position
            //        = new Vector3(sphere.transform.position.x + (capsule.transform.position.x - sphere.transform.position.x) * Time.deltaTime * sphereMagnitudeX,
            //         Mathf.Abs(Mathf.Sin((Time.time - buttonDownTime) * (Mathf.PI) * sphereFrequency) * sphereMagnitudeY), 0);
           //  Debug.Log(Time.time-buttonDownTime);
            //Abs 함수가 없으면 공이 -1~1 까지 튄다.


        }
    }

    float GetRotationAngleByTargetPosition(Vector3 mousePos)
    {
        Vector3 selfScreenPoint = Camera.main.WorldToScreenPoint(capsule.transform.position);
        Vector3 diff = mousePos - selfScreenPoint;

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //2개의 좌표로 1개의 각도를 구할때 Atan2를 사용한다.
        //Atan2 : return value = Radian
        //Radian->Degree 
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
