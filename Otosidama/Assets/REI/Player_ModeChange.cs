using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ModeChange : MonoBehaviour
{
    Animator animator;

    /// <summary>
    /// 主人公の状態　true:勉強中 false:漫画中
    /// </summary>
    public bool P_StudyMode = true;

    /// <summary>
    /// クリックしたオブジェクトを格納する
    /// </summary>
    GameObject clickedGameObject;

    /// <summary>
    /// 主人公がfalseの時に出現させたいGameObject変数
    /// </summary>
    public GameObject FALSEObject;
    public GameObject FALSEObject_2;

    /// <summary>
    /// 主人公がtrueの時に出現させたいGameObject変数
    /// </summary>
    public GameObject TRUEObject;
    public GameObject TRUEObject_2;


    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //クリックしたオブジェクトを取得
        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
            }

            if (clickedGameObject.name == "Ch_0CP00M00Bn")      //クリックしたオブジェクトの名前が主人公オブジェクトの名前だったら
            {
                f_ButtonClick();        //関数呼び出し
            }
        }
    }


    /// <summary>
    /// 主人公の状態を切り替える関数
    /// </summary>
    public bool f_ButtonClick()
    {
        //主人公の状態が勉強中(true)だったとき
        if (P_StudyMode == true)
        {
            Debug.Log("trueの条件式に入った");
            P_StudyMode = false;        //主人公の状態を漫画中(false)にする

            //false用のオブジェクトをActive状態にする
            FALSEObject.SetActive(true);//漫画本をActive状態にする
            FALSEObject_2.SetActive(true);//漫画本をActive状態にする

            //true用のオブジェクトを非Active状態にする
            TRUEObject.SetActive(false);//鉛筆を非Active状態にする
            TRUEObject_2.SetActive(false);//鉛筆を非Active状態にする
        }
        else
        {
            Debug.Log("elseの条件式に入った");
            P_StudyMode = true;        //主人公の状態を勉強中(true)にする

            //false用のオブジェクトを非Active状態にする
            FALSEObject.SetActive(false);//漫画本を非Active状態にする
            FALSEObject_2.SetActive(false);//漫画本を非Active状態にする

            //true用のオブジェクトをActive状態にする
            TRUEObject.SetActive(true);//鉛筆をActive状態にする
            TRUEObject_2.SetActive(true);//鉛筆をActive状態にする
        }

        animator.SetBool("StudyMode", P_StudyMode);
        Debug.Log("主人公の状態"+P_StudyMode);

        return P_StudyMode;             //現在の主人公の状態を返す。
    }
}
