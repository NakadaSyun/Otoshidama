using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDoor : MonoBehaviour
{
    // Start is called before the first frame update

    private delegate void DoorStatus();
    private delegate void CatStatus();
    private struct iArray
    {
        public DoorStatus Dstatus;
        public CatStatus Cstatus;
    }
    iArray[] array = new iArray[5];

    [SerializeField]private GameObject Door;
    [SerializeField]private GameObject Knob;
    [SerializeField]private GameObject Cat;

    [SerializeField]private float Speed = 1;

    private float TimeKeper;

    public bool IsInit;     //初期化
    void Start()
    {
        TimeKeper = 0.0f;
        Initialized();
        IsInit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeKeper += Time.deltaTime * Speed;

        if ((int)TimeKeper < array.Length &&
            array[(int)TimeKeper].Dstatus != null)
        {
            array[(int)TimeKeper].Dstatus();
        }

        if ((int)TimeKeper < array.Length &&
            array[(int)TimeKeper].Cstatus != null)
        {
            array[(int)TimeKeper].Cstatus();
        }

        if (IsInit)
        {
            TimeKeper = 0.0f;
        }
    }

    void OpenDoor()
    {
        // １秒でZ軸を-90度回す
        Door.transform.Rotate(new Vector3(0, 0, -90.0f * (Time.deltaTime * Speed)));
    }

    void DownKnob()
    {
        // １秒でX軸を-30度回す
        Knob.transform.Rotate(new Vector3(-30.0f * (Time.deltaTime * Speed), 0, 0));
    }
    void CloseDoor()
    {
        // １秒でZ軸を-90度回す
        Door.transform.Rotate(new Vector3(0, 0, 90.0f * (Time.deltaTime * Speed)));
    }

    void UpKnob()
    {
        // １秒でX軸を-30度回す
        Knob.transform.Rotate(new Vector3(30.0f * (Time.deltaTime * Speed), 0, 0));
    }

    void CatIn()
    {
        Cat.transform.position += new Vector3(0, 0, -1 * (Time.deltaTime * Speed));
    }

    void CatOut()
    {
        Cat.transform.position += new Vector3(0, 0, 1 * (Time.deltaTime * Speed));
    }


    private void Initialized()
    {
        array[0].Dstatus = DownKnob;
        array[1].Dstatus = OpenDoor;
        array[2].Dstatus = null;
        array[3].Dstatus = UpKnob;
        array[4].Dstatus = CloseDoor;

        array[0].Cstatus = null;
        array[1].Cstatus = CatIn;
        array[2].Cstatus = null;
        array[3].Cstatus = null;
        array[4].Cstatus = CatOut;
    }
}
