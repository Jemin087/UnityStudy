using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float speed = 5f;
    float h = 0f;
    float v = 0f;


    [SerializeField]
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
        PlayerMove();
    }

    void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = h * Vector3.right + v * Vector3.forward;

        transform.Translate(moveDir.normalized * Time.deltaTime * speed);

        transform.LookAt(Vector3.forward);
    }
}
