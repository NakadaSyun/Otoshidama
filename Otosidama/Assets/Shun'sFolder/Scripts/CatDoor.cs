using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDoor : MonoBehaviour
{
    // Start is called before the first frame update

    private delegate void DoorStatus();
    private delegate void CatStatus();

    private bool CanMove;
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
    public bool IsOnce;

    //SE
    public AudioClip CatCry;    //猫の鳴き声
    public AudioClip Open;      //扉を開ける音
    public AudioClip Close;     //扉が閉まる音
    public AudioSource CatSource;
    private bool CatSourcefig;
    private bool Openfig;
    private bool Closefig;
    void Start()
    {
        TimeKeper = 0.0f;
        Initialized();
        IsInit = false;
        CanMove = false;
        IsOnce = false;
        CatSourcefig = false;
        Openfig = false;
        Closefig = false;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            CatSource.Pause();
        }
        else if (Time.timeScale != 0)
        {
            CatSource.UnPause();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsInit)
        {
            TimeKeper = 0.0f;
            CanMove = true;
            IsOnce = true;
            IsInit = false;
            CatSourcefig = false;
            Openfig = false;
            Closefig = false;
        }

        if (!CanMove)return;

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
            CanMove = true;
            IsInit = false;
        }

        //一度だけ鳴らす
        if ((int)TimeKeper > 1 && !CatSourcefig)
        {
            CatSource.PlayOneShot(CatCry);
            CatSourcefig = true;
        }

        if(TimeKeper > 5.0f)
        {
            //一度だけ鳴らす
            if (!Closefig)
            {
                CatSource.PlayOneShot(Close);
                Closefig = true;
            }
            CanMove = false; 
            if (IsOnce)
            {
                GameObject.Find("MainManager").GetComponent<MainManager>().chickenAttackStop();
                IsOnce = false;
            }
        }
    }

    void OpenDoor()
    {
        //一度だけ鳴らす
        if (!Openfig)
        {
            CatSource.PlayOneShot(Open);
            Openfig = true;
        }
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
