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
        Pause       // ポーズ
    }
    [SerializeField] public Scene scene;


    GameObject UIObj;

    void Start()
    {
        UIObj = GameObject.Find("UI_Script");
        scene = Scene.Main;
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
        Time.timeScale = 0;
        UIObj.GetComponent<ButtonScript>().GameOver.SetActive(true);
        UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
        UIObj.GetComponent<ButtonScript>().GameClear.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
        UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
    }

    void GameClear()
    {
        Time.timeScale = 0;
        UIObj.GetComponent<ButtonScript>().GameClear.SetActive(true);
        UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
        UIObj.GetComponent<ButtonScript>().GameOver.SetActive(false);
        //UIObj.GetComponent<ButtonScript>().Title.SetActive(false);
        UIObj.GetComponent<ButtonScript>().Pause.SetActive(false);
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

    public void Init()
    {
        Time.timeScale = 1;
        UIObj.GetComponent<timeCount>().TimeReset();// カウントの初期化
    }
}
