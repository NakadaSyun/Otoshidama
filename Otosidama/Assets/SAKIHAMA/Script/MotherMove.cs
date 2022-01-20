using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherMove : MonoBehaviour
{
    public Animator anima;      //ドアのアニメーション
    private float time;         //タイム変数
    int Count = 0;              //カウント変数
    //public bool Gflg;                  //仮フラグ

    //void Start()
    //{
    //    //仮
    //    Gflg = false;
    //}

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M) && Gflg == false)
        //{
        //    Gflg = true;
        //}

        //if (Gflg == true) { 
            time += Time.deltaTime;

            //ドアノブの処理
            if (time > 1 && anima.GetBool("Transfer") == false)
            {
                //ドアノブのBoolを逆に変更
                anima.SetBool("Turn", !anima.GetBool("Turn"));
                //カウントアップする
                Count++;
                //タイムを0に戻す
                time = 0;
            }

            //5秒間の処理

            //ドアノブが3回下がったらドアが開く
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
            }

            //ドアが開き始めてからの時間を計る
            if (anima.GetBool("Transfer") == true)
            {
                //ドアが開いている時間を計測
                time += Time.deltaTime;
            }

            ////アニメーション移行flgがtrueならなおかつ1回置いて
            if (anima.GetBool("Transfer") == true && time > 5/*Count > 1*/)
            {
                //ドアのBoolを逆に変更し閉じる
                anima.SetBool("Open", !anima.GetBool("Open"));
            }

        //ドアのflgがfalseなおかつアニメーション移行flgがtrueなら入る(ドアが閉じる時の処理)
        if (anima.GetBool("Open") == false && anima.GetBool("Transfer") == true)
        {
            //アニメーション移行flgをfalseにする
            anima.SetBool("Transfer", false);
            //タイムを0に戻す
            time = 0;
            //Gflg = false;
            //mother.SetActive(false);
        }
        //}
    }
}
