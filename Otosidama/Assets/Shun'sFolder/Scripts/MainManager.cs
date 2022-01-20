using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Friend;
    [SerializeField] private GameObject Chicken;

    private delegate void Wave();
    private struct iArray
    {
        public Wave wave;
    }
    iArray[] array = new iArray[5];

    private float TimeLimit;
    private float TimeCount;

    public bool canParentFind;        //親の判定(true:ON false:OFF)
    public bool canFriendFind;        //友達の判定(true:ON false:OFF)
    public bool Plstatus;        //親or友達の判定(true:勉強 false:さぼり)
    void Start()
    {
        TimeCount = GameObject.Find("UI_Script").GetComponent<timeCount>().countup;
        TimeLimit = GameObject.Find("UI_Script").GetComponent<timeCount>().timeLimit;
        Initialized();


        GameObject.Find("Door").GetComponent<MotherMove>().enabled = false;     //親の攻撃をstop
    }

    // Update is called once per frame
    void Update()
    {
        WaveFunc();


    }

    void WaveFunc()
    {
        int waveNum = (int)((TimeLimit - TimeCount) / 12) - 1;
        array[waveNum].wave();
    }

     public bool checkFind()
    {
        if(canParentFind && !Plstatus)  //親が見ていてさぼっている場合
        {
            return true;
        }
        
        if(canFriendFind && Plstatus)  //友達が見ていて勉強している場合
        {
            return true;
        }

        return false;
    }

    private void Initialized()
    {
        array[0].wave = Wave1;
        array[1].wave = Wave2;
        array[2].wave = Wave3;
        array[3].wave = Wave4;
        array[4].wave = Wave5;
    }

    void Wave1()
    {

    }

    void Wave2()
    {

    }

    void Wave3()
    {

    }

    void Wave4()
    {

    }

    void Wave5()
    {

    }

    void parentAttackStart()
    {
        //親の攻撃がStart
        GameObject.Find("Door").GetComponent<MotherMove>().enabled = true;
        canParentFind = true;
    }
    void parentAttackStop()
    {
        //親の攻撃がStop
        canParentFind = false;
    }

    void friendAttackStart()
    {
        //友達の攻撃がStart
        Friend.GetComponent<FriendsMove>().Start();
        canFriendFind = true;

    }

    void friendAttackStop()
    {
        //友達の攻撃がStop
        canFriendFind = false;
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
}
