using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject attackFX;


    float speed = 5f;
    float h;
    float v;

    float rotSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Moving();
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 3);

            attackFX.transform.position = pos;
           
            Instantiate(attackFX);
        }
    }


    void Moving()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);

        transform.Translate(dir.normalized * Time.deltaTime * speed, Space.Self);
    }

    void Rotation()
    {
        
    }
}
