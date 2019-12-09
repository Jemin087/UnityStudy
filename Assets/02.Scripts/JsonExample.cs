using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;



[System.Serializable]
public class JTestClass
{
    public int i;
    public float f;
    public bool b;
    public Vector3 v;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();

    public JTestClass() { }

    public JTestClass(bool isSet)
    {
        if (isSet)
        {
            i = 10;
            f = 99.9f;
            b = true;
            v = new Vector3(39.56f, 21.2f, 6.4f);
            str = "JSON Test String";
            iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };
            for (int i = 0; i < 5; i++)
            {
                iList.Add(2 * i);
            }
        }
    }
    public void Print()
    {
        Debug.Log("i = " + i);
        Debug.Log("f = " + f);
        Debug.Log("b = " + b);
        Debug.Log("v = " + v);
        Debug.Log("str = " + str);

        for (int i = 0; i < iArray.Length; i++)
        {
            Debug.Log(string.Format("iArray[{0}] = {1} ", i, iArray[i]));
        }

        for (int i = 0; i < iList.Count; i++)
        {
            Debug.Log(string.Format("iList[{0}] = {1} ", i, iList[i]));
        }

    }
}

[System.Serializable]
public class UJsonTester
{
    public Vector3 v3;
    public UJsonTester() { }

    public UJsonTester(float f)
    {
        v3 = new Vector3(f, f, f);
    }

    public UJsonTester(Vector3 v)
    {
        v3 = v;
    }

}


//MonoBehaviour 상속받는 클래스는 반드시
//GetComponent() 등으로 직접 가져온 클래스로 시리얼라이즈를 해야한다.
[System.Serializable]
public class TestMono : MonoBehaviour
{
    public int i = 10;
    public Vector3 pos = new Vector3(1f, 2f, 3f);
}




public class JsonExample : MonoBehaviour
{
    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    T LoadJsonFile<T>(string loadPath,string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new GameObject();

        obj.name = "TestMono 01";
        var t = obj.AddComponent<TestMono>();

        t.i = 333;
        t.pos = new Vector3(-939, -33, 22);
        var jd = JsonUtility.ToJson(obj.GetComponent<TestMono>());
        Debug.Log(jd);

        GameObject obj2 = new GameObject();
        obj2.name = "TestMono 02";
        var t2 = obj2.AddComponent<TestMono>();
        JsonUtility.FromJsonOverwrite(jd, t2);
     
    }

    void CreateJsonFile(string createPath,string fileName,string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
