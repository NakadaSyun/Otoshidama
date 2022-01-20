using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] public GameObject Title;
    [SerializeField] public GameObject GameClear;
    [SerializeField] public GameObject GameOver;
    [SerializeField] public GameObject MainCanvas;
    [SerializeField] public GameObject Pause;

    GameObject UIObj;

    void Start()
    {
        UIObj = GameObject.Find("UI_Script");

        Pause.SetActive(false);
        MainCanvas.SetActive(false);
        GameClear.SetActive(false);
        GameOver.SetActive(false);
        Title.SetActive(true);

        Time.timeScale = 0;
    }

    public void StartButton()
    {
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
        UIObj.GetComponent<NextScene>().Init();
    }

    public void PauseButton_ON()
    {
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Pause);
    }
    public void PauseButton_OFF()
    {
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
    }

    public void RetryButton()
    {
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
        UIObj.GetComponent<NextScene>().Init();
    }

    public void BackTitleButton()
    {
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Title);
        UIObj.GetComponent<NextScene>().Init();
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif // UNITY_EDITOR
    }
}
