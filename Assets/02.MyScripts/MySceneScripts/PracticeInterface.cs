using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventHandler();


abstract public class A : MonoBehaviour
{
    abstract public void ABC();
}

interface IMethod
{
    void BBC();
}

interface IProperty
{
    int salaryP { get; set; }
}

interface IIndexer
{
    int this[int index]
    {
        get;set;
    }
    
}

interface IEvent
{
    event EventHandler OnDie;
}

public class PracticeInterface : A,IMethod,IProperty,IIndexer,IEvent
{
    private int[] arr = new int[100];

    private int salary;

    public event EventHandler OnDie;
    public event EventHandler OnStart;

    public int salaryP
    {
        get { return salary; }
        set { salary = value; }
    }

    public int this[int index]
    {
        get { return arr[index]; }
        set { arr[index] = value; }
    }


    public override void ABC()
    {
        print("ABC");
    }

    public void BBC()
    {
        print("BBC");
    }

    private void EndGame()
    {
        print("Game End");
    }

    private void StartGame()
    {
        print("Game Start");
    }

    void Start()
    {
        OnStart += StartGame;

        OnStart();

        ABC();
        BBC();
        salaryP = 10;
        print(salaryP);

        this[5] = 20;
        print(this[5]);

        OnDie += EndGame;
        OnDie();
    }


}




