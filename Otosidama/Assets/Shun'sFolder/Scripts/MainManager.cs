using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject Chicken;
    private const float BREAK_TIME = 1.0f;
    private const float RAID_TIME = 6.0f;
    private float period;

    private delegate void Event();
    private struct iArray
    {
        public Event Raidevent;
    }
    iArray[] array = new iArray[4];


    public bool canParentFind;        //親の判定(true:ON false:OFF)
    public bool canFriendFind;        //友達の判定(true:ON false:OFF)
    public bool Plstatus;        //親or友達の判定(true:勉強 false:さぼり)

    private bool IsRaid;
    void Start()
    {
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = false;     //親の攻撃をstop

        Initialized();

        canParentFind = false;
        canFriendFind = false;
        Plstatus = false;

        IsRaid = false;

        period = BREAK_TIME;
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
        if (!IsRaid)
        {
            period -= Time.deltaTime;
            if(period < 0.0f)
            {
                array[0].Raidevent();
                IsRaid = true;
                period = RAID_TIME;
            }
        }
        //襲撃イベントの実行中
        else if(IsRaid)
        {
            period -= Time.deltaTime;
            if (period < 0.0f)
            {
                IsRaid = false;
                period = BREAK_TIME;
            }
        }
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
        //親の攻撃がStart

        GameObject.Find("Door").GetComponent<MotherMove>().enabled = true;
        Invoke("parentAttackStop", 5.5f);
    }
    void parentAttackStop()
    {
        //親の攻撃がStop
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = false;
    }

    void friendAttackStart()
    {
        //友達の攻撃がStart
        Friend.GetComponent<FriendsMove>().Init = true;
    }

    void friendAttackStop()
    {
    }

    void CatAttack()
    {
        //猫の攻撃がStart
        gameObject.GetComponent<CatDoor>().IsInit = true;
    }

    void chickenAttack()
    {
        //鶏の攻撃がStart
        Chicken.GetComponent<ChickenFeint>().PosInit();
    }

    public bool checkFind()
    {
        Debug.Log(canParentFind);
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
