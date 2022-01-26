using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherMove : MonoBehaviour
{
    public Animator anima;      //ドアのアニメーション
    private float time;         //タイム変数
    private float Ptime;        //アニメーションの最初のラグ修正タイム変数
    int Count = 0;              //カウント変数
    public AudioClip Twist;     //ドアノブを捻る音
    public AudioClip Open;      //扉を開ける音
    public AudioClip Close;     //扉を閉める音

    private bool IsOnce;

    public AudioSource DoorSource;

    void Start()
    {
        //カウントの初期化
        Count = 0;
        //ラグ修正タイム変数
        Ptime = 0.1f;

        IsOnce = true;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            DoorSource.Pause();
        }
        else if (Time.timeScale != 0)
        {
            DoorSource.UnPause();
        }
    }
    void FixedUpdate()
    {
        if (anima.GetBool("Transfer") == false)
        {
            //ドアが開いている時間を計測
            time += Time.deltaTime;
        }

        //5秒間の処理

        //ドアノブの処理
        if (time > 0.3+ Ptime && anima.GetBool("Transfer") == false)
        {
            //ドアノブのBoolを逆に変更
            anima.SetBool("Turn", !anima.GetBool("Turn"));
            //カウントアップする
            Count++;
            //タイムを0に戻す
            time = 0;
            //音を鳴らす
            if ((Count + 1) % 2 == 0)
            {
                //音
                DoorSource.PlayOneShot(Twist);
            }
            //ラグ修正タイムを0に設定
            Ptime = 0;
        }

        //ドアノブが2回下がったらドアが開く
        if (Count / 2 > 2)
        {
            //アニメーション移行flgをtrue
            anima.SetBool("Transfer", true);
            //ドアのBoolを逆に変更し閉じる
            anima.SetBool("Open", !anima.GetBool("Open"));
            //タイムを0に戻す
            time = 0;
            //カウントを0に戻す
            Count = 0;
            //音
            DoorSource.PlayOneShot(Open);
        }

       
        //ドアのflgがfalseなおかつアニメーション移行flgがtrueなら入る(ドアが閉じる時の処理)
        if (anima.GetBool("Open") == false && anima.GetBool("Transfer") == true)
        {
            GameObject.Find("MainManager").GetComponent<MainManager>().canParentFind = false;
            GameObject.Find("MainManager").GetComponent<MainManager>().parentAttackStop();
            //アニメーション移行flgをfalseにする
            anima.SetBool("Transfer", false);

            //タイムを0に戻す
            time = 0;
            IsOnce = true;

        }
        ////アニメーション移行flgがtrueならなおかつ1回置いて
        else if (anima.GetBool("Transfer") == true && time > 3.5)
        {
            Debug.Log("MotherMove");
            //ドアのBoolを逆に変更し閉じる
            anima.SetBool("Open", false);
            //音
            DoorSource.PlayOneShot(Close);
        } 
        //ドアが開き始めてからの時間を計る(この間、母親は見ている)
        else if (anima.GetBool("Transfer") == true)
        {
            if (IsOnce && time > 0.5f)
            {
                GameObject.Find("MainManager").GetComponent<MainManager>().canParentFind = true;
                IsOnce = false;
            }
            //ドアが開いている時間を計測
            time += Time.deltaTime;
        }
    }
}
