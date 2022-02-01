using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    // 現在のシーン情報
    public enum Scene
    {
        Title,      // タイトル
        Main,       // メイン
        GameOver,   // ゲームオーバー
        GameClear,  // ゲームクリア
        Pause,      // ポーズ
        ThreeCount
    }
    [SerializeField] public Scene scene;

    public ParticleSystem Kamihubuki;       //クリア時の紙吹雪のパーティクル

    GameObject UIObj;

    private float anim_cnt;     //GameOver時のアニメーション再生までのカウント変数

    /// <summary>
    /// GameOver時のUI出現までの待機時間
    /// </summary>
    public float GameOverUI_WaitTime = 1.5f;

    /// <summary>
    /// GameClear時のUI出現までの待機時間
    /// </summary>
    public float GameClearUI_WaitTime = 1.5f;
    public AudioClip Over;
    public AudioClip Clear;
    public AudioSource AudioS;
    private bool OverFig;
    private bool ClearFig;

    [SerializeField] private GameObject pauseButton;

    void Start()
    {
        Time.timeScale = 0;
        UIObj = GameObject.Find("UI_Script");
        scene = Scene.ThreeCount;
        OverFig = false;
        ClearFig = false;
    }

    void Update()
    {
        switch (scene)
        {
            case Scene.Title:
                Title();
                break;
            case Scene.Main:
                Main();
                break;
            case Scene.GameOver:
                GameOver();
                break;
            case Scene.GameClear:
                GameClear();
                break;
            case Scene.Pause:
                Pause();
                break;
            case Scene.ThreeCount:
                ThreeTimeCount();
                break;
        }
    }

    public void SceneChange(Scene sceneName)
    {
        scene = sceneName;

    }

    void Title()
    {
        //Time.timeScale = 0;
        //UIObj.GetComponent<ButtonScript>().Title.SetActive(true);
        //UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
    }

    void Main()
    {
        Time.timeScale = 1;
        if(UIObj != null)
        {
            UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(true);
            //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
            UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
            UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
            UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
        }
    }

    void GameOver()
    {
        //GameOver時のUI出現までの時間をカウント開始
        anim_cnt += Time.deltaTime;

        //決められたUI出現までの時間が経過したら
        if (anim_cnt > GameOverUI_WaitTime)
        {
            if (!OverFig)
            {
                AudioS.PlayOneShot(Over);
                OverFig = true;
            }

            Time.timeScale = 0;
            UIObj.GetComponent<ButtonScript>().GameOver.SetActive(true);
            UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
            UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
            //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
            UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
        }
    }

    void GameClear()
    {
        Kamihubuki.Simulate(t: Time.unscaledDeltaTime, //パーティクルシステムを早送りする時間
                                   withChildren: true,                   //子のパーティクルシステムもすべて早送りするかどうか
                                   restart: false);                   //再起動し最初から再生するかどうか

        //GameClear時のUI出現までの時間をカウント開始
        anim_cnt += Time.deltaTime;

        //決められたUI出現までの時間が経過したら
        if (anim_cnt > GameClearUI_WaitTime)
        {
            if (!ClearFig)
            {
                AudioS.PlayOneShot(Clear);
                ClearFig = true;

                GameObject.Find("MainManager").GetComponent<MainManager>().IsGameEnd = true;
            }
            Time.timeScale = 0;
            UIObj.GetComponent<ButtonScript>().GameClear.SetActive(true);
            UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
            UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
            //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
            UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        UIObj.GetComponent<ButtonScript>().Pause.SetActive(true);
        UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(true);
        UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
        UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
    }

    void ThreeTimeCount()
    {
        pauseButton.SetActive(false);
        UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
        UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(true);
        UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
        UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
        if(UIObj.GetComponent<ThreeCount>().StartFlg == false)
        {
            // コルーチンを一度だけ呼び出す
            StartCoroutine(UIObj.GetComponent<ThreeCount>().Count());
        }
        if(Time.timeScale == 1)
        {
            pauseButton.SetActive(true);
            scene = Scene.Main;
        }

    }

    public void Init()
    {
        Time.timeScale = 1;
        UIObj.GetComponent<timeCount>().TimeReset();// カウントの初期化
    }
}
