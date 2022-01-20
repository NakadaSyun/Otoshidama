using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//未使用

public class nome : MonoBehaviour
{
    //private int Playtime = 5;    //再生時間
    //private float time = 0;            //時間を格納
    public Animator anima;       //母親のアニメーション
    public bool MotherFig;     //何にも使っていない何かに使っていい奴
    private GameObject Mother;  //ママのオブジェクト情報を格納
    //public GameObject Door;     //ドアのオブジェクト情報を格納

    // Start is called before the first frame update
    void Start()
    { 
        MotherFig = false;
        Mother = this.gameObject;
        //仮
        Mother.SetActive(true);
    }

    void Update()
    {
        //MotherFigがtrueの場合処理開始
        if (Mother.activeSelf == true)
        {
            if(Input.GetKey(KeyCode.L))
            {
                Mother.transform.position -= new Vector3(0, 0, 0.1f);
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                anima.SetBool("Move", !anima.GetBool("Move"));
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                anima.SetBool("Looking", !anima.GetBool("Looking"));
            }
        }
  
    }

    ////母親処理
    //void MotherMove()
    //{

    //}
}

