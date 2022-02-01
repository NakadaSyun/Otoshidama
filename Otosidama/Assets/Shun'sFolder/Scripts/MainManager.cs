using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject Chicken;
    [SerializeField] private GameObject UI_Obj;
    private const float BREAK_TIME = 1.0f;
    private const float RAID_TIME = 12.0f;
    private float period;
    public bool IsGameEnd;

    private delegate void Event();
    private struct iArray
    {
        public Event Raidevent;
    }
    iArray[] array = new iArray[4];


    public bool canParentFind;        //親の判定(true:ON false:OFF)
    public bool canFriendFind;        //友達の判定(true:ON false:OFF)
    public bool Plstatus;        //親or友達の判定(true:勉強 false:さぼり)

    public bool IsRaid;

    public GameObject Mother;   //母親
    public GameObject Cat;      //猫
    private bool CatAndMotherfig;
    void Start()
    {
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = false;     //親の攻撃をstop

        Initialized();

        canParentFind = false;
        canFriendFind = false;
        Plstatus = false;

        IsRaid = false;
        CatAndMotherfig = true;

        period = BREAK_TIME;

        IsGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        Plstatus = GameObject.Find("Player").GetComponent<Player_ModeChange>().P_StudyMode;
        Raid();
    }

    int MakeRaid()
    {
        //襲撃イベントの選定
        int RaidNum = 0;

        RaidNum = Random.Range(0, 120) % 4;

        return RaidNum;
    }
    void Raid()
    {
        //襲撃イベントの実行
        if (!IsRaid && !IsGameEnd
            && 
            (UI_Obj.GetComponent<EnergyGauge>().stadyGauge.value <= 0.98
            || UI_Obj.GetComponent<EnergyGauge>().gameGauge.value <= 0.98))
        {
           array[MakeRaid()].Raidevent();
            IsRaid = true;
        }
        //襲撃イベントの実行中
        else if(IsRaid)
        {}
    }

    private void Initialized()
    {
        array[0].Raidevent = parentAttackStart;
        array[1].Raidevent = friendAttackStart;
        array[2].Raidevent = CatAttack;
        array[3].Raidevent = chickenAttack;
    }

        void parentAttackStart()
    {
        CatAndMotherfig = false;
        Cat.SetActive(false);
        //親の攻撃がStart
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = true;
    }
    public void parentAttackStop()
    {
        //親の攻撃がStop
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = false;
        IsRaid = false;
    }

    void friendAttackStart()
    {
        //友達の攻撃がStart
        Friend.GetComponent<FriendsMove>().Init = true;
    }

    public void friendAttackStop()
    {
        IsRaid = false;
    }

    void CatAttack()
    {
        Cat.SetActive(true);
        if (CatAndMotherfig)
        {
            Mother.SetActive(false);
            CatAndMotherfig = false;
        }
        //猫の攻撃がStart
        gameObject.GetComponent<CatDoor>().IsInit = true;
        
    }
    public void CatAttackStop()
    {
        //猫の攻撃がStop
        IsRaid = false;
    }

    void chickenAttack()
    {
        //鶏の攻撃がStart
        Chicken.GetComponent<ChickenFeint>().PosInit();
    }

    public void chickenAttackStop()
    {
        //鶏の攻撃がStop
        IsRaid = false;
    }

    public bool checkFind()
    {
        //親が見ているときにさぼる
        if (canParentFind == true && Plstatus == false)
        {
            return true;
        }

        //友達が見ているときに勉強
        if (canFriendFind == true && Plstatus == true)
        {
            return true;
        }

        return false;
    }
}
