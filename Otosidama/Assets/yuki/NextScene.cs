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
    [SerializeField] private Scene scene;


    GameObject UIObj;

    void Start()
    {
        UIObj = GameObject.Find("UI_Script");
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
                break;
        }
    }

    public void SceneChange(Scene sceneName)
    {
        scene = sceneName;
    }

    void Title()
    {

    }

    void Main()
    {

    }

    void GameOver()
    {

    }

    void GameClear()
    {
        UIObj.GetComponent<ButtonScript>().GameClear.SetActive(true);
        UIObj.GetComponent<ButtonScript>().MainCanvas.SetActive(false);
    }
}
