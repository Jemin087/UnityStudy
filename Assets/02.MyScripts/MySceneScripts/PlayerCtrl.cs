using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    float speed = 10f;
    float h,v;

    //float mx, my;

    //Vector3 rotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Casting.instance.OnCasting();
        }

        PlayerMove();
        PlayerRotate();
    }

   


    void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //mx = Input.GetAxisRaw("Mouse X");
        //my = Input.GetAxisRaw("Mouse Y");

        //rotation = new Vector3(0f, my, 0f) * 5f;
        


        Vector3 moveDir = h * Vector3.right + v * Vector3.forward;

        transform.Translate(moveDir.normalized * Time.deltaTime * speed);

        //transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //transform.LookAt(Vector3.forward);
    }

    void PlayerRotate()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, transform.rotation.y-90f, 0);
            //transform.LookAt(Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, transform.rotation.y+90f, 0);
           // transform.LookAt(Vector3.forward);
        }
    }

}
