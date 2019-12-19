using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyChapter02 : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float scrollSpeed = 200f;

    public Transform pivot;

    //SphericalCoordinates : 구면좌표
    [System.Serializable]
    public class SphericalCoordinates
    {
        public float radius, azimuth, elevation;

        public float minRadius = 3f;
        public float maxRadius = 20f;

        public float minAzimuth = 0f;
        public float maxAzimuth = 360f;

        private float _minAzimuth;
        private float _maxAzimuth;

        public float minElevation = 0f;
        public float maxElevation = 90f;

        private float _minElevation;
        private float _maxElevation;

        //구면좌표에서의 반지름 (카메라와 큐브의 거리)
        public float Radius
        {
            get { return radius; }
            private set
            {
                radius = Mathf.Clamp(value, minRadius, maxRadius);
            }
        }

        //방위각 카메라가 360도 돌아도 상관x
        public float Azimuth
        {
            get { return azimuth; }
            private set
            {
                azimuth = Mathf.Repeat(value, _maxAzimuth - _minAzimuth);
            }
        }

        //카메라 각도 
        public float Elevation
        {
            get { return elevation; }
            private set
            {

                elevation = Mathf.Clamp(value, _minElevation, _maxElevation);
            }
        }

        public SphericalCoordinates() { }

        //cartesianCoordinate : 직교좌표(데카르트 좌표)
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            _minAzimuth = Mathf.Deg2Rad * minAzimuth;
            _maxAzimuth = Mathf.Deg2Rad * maxAzimuth;

           
            _minElevation = Mathf.Deg2Rad * minElevation;
            _maxElevation = Mathf.Deg2Rad * maxElevation;
 

            Radius = cartesianCoordinate.magnitude;
            Azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            Elevation = Mathf.Asin(cartesianCoordinate.y / Radius);
        }

        //직교좌표로 변환
        public Vector3 toCartesian
        {
            get
            {
                float t = Radius * Mathf.Cos(Elevation);
                return new Vector3(t * Mathf.Cos(Azimuth), Radius * Mathf.Sin(Elevation), t * Mathf.Sin(Azimuth));
            }
        }

        //카메라 회전
        public SphericalCoordinates Rotate(float newAzimuth, float newElevation)
        {
            Azimuth += newAzimuth;
            Elevation += newElevation;
            return this;
        }

        //카메라와 큐브사이의 거리 조절
        public SphericalCoordinates TranslateRadius(float x)
        {
            Radius += x;
            return this;

        }
    }

    public SphericalCoordinates sphericalCoordinates;
    // Start is called before the first frame update
    void Start()
    {
        sphericalCoordinates = new SphericalCoordinates(transform.position);
        transform.position = sphericalCoordinates.toCartesian + pivot.position;
    }

    // Update is called once per frame
    void Update()
    {
        float kh, kv, mh, mv, h, v;

        kh = Input.GetAxis("Horizontal");
        kv = Input.GetAxis("Vertical");

        bool anyMouseButton = Input.GetMouseButton(0);
        mh = anyMouseButton ? Input.GetAxis("Mouse X") : 0f;
        mv = anyMouseButton ? Input.GetAxis("Mouse Y") : 0f;

        
        h = kh * kh > mh * mh ? kh : mh;
        v = kv * kv > mv * mv ? kv : mv;

        //입력이 있으면
        if(h*h>Mathf.Epsilon||v*v>Mathf.Epsilon)
        {
            transform.position 
                = sphericalCoordinates.Rotate(h * rotateSpeed * Time.deltaTime, v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;

        }

        float sw = -Input.GetAxis("Mouse ScrollWheel");

        //마우스 휠 입력이 있을 경우 
        if(sw*sw>Mathf.Epsilon)
        {
            transform.position 
                = sphericalCoordinates.TranslateRadius(sw * Time.deltaTime * scrollSpeed).toCartesian + pivot.position;
        }
        //카메라는 큐브를 바라본다.
        transform.LookAt(pivot.position);


    }

}