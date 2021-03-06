using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlinePlayer_ModeChange : MonoBehaviourPunCallbacks, IPunObservable
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

    /// <summary>
    /// シーン変更するスクリプトがアタッチされているオブジェ格納変数
    /// </summary>
    GameObject Sceneobj;
    /// <summary>
    /// シーン状態がenum管理されているスクリプト格納変数
    /// </summary>
    NextScene Scenescript;

    //public AudioClip GameC1;
    //public AudioClip GameC2;
    //public AudioClip StudyC;
    //public AudioSource PlayerS;
    public AudioSource GameS1;
    public AudioSource GameS2;
    public AudioSource StudyS;
    public AudioSource ButtonS;
    public AudioSource SurprisedS;
    public AudioSource ApplauseS;
    private bool Studyfig;      //False:GameS1 True:GamseS2
    private bool Gamefig;
    private bool Surprisedfig;
    private bool Applausefig;


    void Start()
    {
        animator = GetComponent<Animator>();

        Sceneobj = GameObject.Find("UI_Script");
        Scenescript = Sceneobj.GetComponent<NextScene>();

        //GameS1.Play();
        GameS2.Stop();
        StudyS.Stop();
        SurprisedS.Stop();
        ApplauseS.Stop();
        Studyfig = false;
        Gamefig = false;
        Surprisedfig = false;
        Applausefig = false;

    }

    void Update()
    {

        //現在のシーン状態がMain（母親、友達に見つかっていない状態）の時
        if (Scenescript.scene == NextScene.Scene.Main)
        {
            //クリックしたオブジェクトを取得
            if (Input.GetMouseButtonDown(0))
            {
                clickedGameObject = null;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //ヒットしたすべてのオブジェクト情報を取得
                foreach (RaycastHit obj in Physics.RaycastAll(ray))
                {
                    //ヒットしたオブジェクトの名前
                    if (obj.transform.name == GameObject.Find(PhotonNetwork.LocalPlayer.ActorNumber.ToString()).name)      //クリックしたオブジェクトの名前が主人公オブジェクトの名前だったら
                    {
                        if (GetComponent<PhotonView>().IsMine)
                        {
                            f_ButtonClick();        //関数呼び出し
                        }
                    }
                }

                
            }
        }

        //現在のシーンの状態がGameOver（母親、友達に見つかった状態）の時
        if (Scenescript.scene == NextScene.Scene.GameOver)
        {
            if (!Surprisedfig)
            {
                SurprisedS.Play();
                Surprisedfig = true;
            }
            animator.SetBool("Wow_Anim", true); //Wow_Animのアニメーション再生フラグをtureにする
        }

        //現在のシーンの状態がGameClear（勉強、さぼりをやり遂げた状態）の時
        if (Scenescript.scene == NextScene.Scene.GameClear)
        {
            if (!Applausefig)
            {
                ApplauseS.Play();
                Applausefig = true;
            }
            animator.SetBool("Yahoo_Anim", true); //Yahoo_Animのアニメーション再生フラグをtureにする
        }

        //ゲーム中SEの変更
        if (!Studyfig)
        {
            if (!GameS1.isPlaying && !Gamefig)
            {
                ButtonS.Stop();
                GameS1.Stop();
                ButtonS.Play();
                GameS2.Play();
                Gamefig = true;
            }
            else if (!GameS2.isPlaying && Gamefig)
            {
                ButtonS.Stop();
                GameS2.Stop();
                ButtonS.Play();
                GameS1.Play();
                Gamefig = false;
            }
        }

        if (Time.timeScale == 0)
        {
            GameS1.Pause();
            GameS2.Pause();
            StudyS.Pause();
            ButtonS.Pause();
        }
        else if (Time.timeScale != 0)
        {
            GameS1.UnPause();
            GameS2.UnPause();
            StudyS.UnPause();
            ButtonS.UnPause();
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
            int R = (int)Random.Range(1, 10);
            StudyS.Stop();
            if (R < 6)
            {
                GameS1.Play();
                //ButtonS.Play();
                Gamefig = true;
                Studyfig = false;
            }
            else
            {
                GameS2.Play();
                //ButtonS.Play();
                Gamefig = false;
                Studyfig = false;
            }

            Debug.Log("trueの条件式に入った");
            P_StudyMode = false;        //主人公の状態を漫画中(false)にする

            //false用のオブジェクトをActive状態にする
            FALSEObject.SetActive(true);//ゲーム機をActive状態にする
            FALSEObject_2.SetActive(true);//さぼりオブジェクトをActive状態にする

            //true用のオブジェクトを非Active状態にする
            TRUEObject.SetActive(false);//鉛筆を非Active状態にする
            TRUEObject_2.SetActive(false);//勉強オブジェクトを非Active状態にする
        }
        else
        {
            GameS1.Stop();
            GameS2.Stop();
            ButtonS.Stop();
            if (!Studyfig)
            {
                StudyS.Play();
                Studyfig = true;
            }
            Debug.Log("elseの条件式に入った");
            P_StudyMode = true;        //主人公の状態を勉強中(true)にする

            //false用のオブジェクトを非Active状態にする
            FALSEObject.SetActive(false);//ゲーム機を非Active状態にする
            FALSEObject_2.SetActive(false);//さぼりオブジェクトを非Active状態にする

            //true用のオブジェクトをActive状態にする
            TRUEObject.SetActive(true);//鉛筆をActive状態にする
            TRUEObject_2.SetActive(true);//勉強オブジェクトをActive状態にする
        }

        animator.SetBool("StudyMode", P_StudyMode);
        Debug.Log("主人公の状態" + P_StudyMode);

        return P_StudyMode;             //現在の主人公の状態を返す。
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(P_StudyMode);
            Debug.Log(this.name + "送信");
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            P_StudyMode = (bool)stream.ReceiveNext();
            Debug.Log(this.name + "受信");
        }
    }
}
