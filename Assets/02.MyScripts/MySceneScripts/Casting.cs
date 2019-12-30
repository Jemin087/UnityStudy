using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{


    public Image castingBar;

    public bool check = false;
    public bool coru = false;

    public static Casting instance;

    private void Awake()
    {
        instance = this;
    }

    public void OnCasting()
    {
        coru = true;
        Debug.Log("coru : " + coru);
        StartCoroutine(SkillCasting());
    }


    IEnumerator SkillCasting()
    {
        float t = 0;
        float time = 1f;



        while(coru)
        {
            t += Time.deltaTime / time;
            castingBar.fillAmount = Mathf.Lerp(0, 1, t);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                coru = false;

                Debug.Log("coru : " + coru);
            }

            if(check)
            {
                castingBar.color = new Color(1, 1, 1);
                castingBar.fillAmount = 0;
                yield break;

            }
            if(!coru)
            {
                castingBar.color = new Color(1, 1, 1);
                castingBar.fillAmount = 0;
                yield break;
            }
            if(castingBar.fillAmount>0.7&&castingBar.fillAmount<=0.8)
            {
                castingBar.color = new Color(1, 0.5f, 1);
            }
            

            yield return null;
        }

    }


}
