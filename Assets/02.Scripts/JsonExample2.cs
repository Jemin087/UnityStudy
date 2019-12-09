using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JTestClassB
{
    public int i;
    public float f;
    public bool b;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();
    public Dictionary<string, float> fDictionary = new Dictionary<string, float>();

    public JTestClassB() { }

    public JTestClassB(bool isSet)
    {
        if(isSet)
        {
            i = 10;
            f = 99.9f;
            b = true;
            str = "JSON Test String";
            iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };
            for(int i=0; i<5; i++)
            {
                iList.Add(2 * i);
            }

            fDictionary.Add("PIE", Mathf.PI);
            fDictionary.Add("Epsilon", Mathf.Epsilon);
            fDictionary.Add("Sqrt(2)", Mathf.Sqrt(2));

        }
    }

    public void Print()
    {
        Debug.Log("i = " + i);
        Debug.Log("f = " + f);
        Debug.Log("b = " + b);
        Debug.Log("str = " + str);

        for(int i=0; i<iArray.Length; i++)
        {
            Debug.Log(string.Format("iArray[{0}] = {1}", i, iArray[i]));

        }
        for(int i=0; i<iList.Count; i++)
        {
            Debug.Log(string.Format("iList[{0}] = {1} ", i, iList[i]));
        }

        foreach(var data in fDictionary)
        {
            Debug.Log(string.Format("iDictionary[{0}] = {1}", data.Key, data.Value));

        }

    }
}


public class JsonExample2 : MonoBehaviour
{

    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    T JSonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    // Start is called before the first frame update
    void Start()
    {
        JTestClassB jtc = new JTestClassB(true);
        string jsonData = ObjectToJson(jtc);

        Debug.Log(jsonData);

        var jtc2 = JSonToObject<JTestClassB>(jsonData);
        jtc2.Print();
    }

    
}
