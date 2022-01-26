using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //[SerializeField] public GameObject Title;
    [SerializeField] public GameObject GameClear;
    [SerializeField] public GameObject GameOver;
    [SerializeField] public GameObject MainCanvas;
    [SerializeField] public GameObject Pause;

    GameObject UIObj;

    public AudioClip Top;
    public AudioClip On;
    public AudioSource ButtonS;

    void Start()
    {
        UIObj = GameObject.Find("UI_Script");

        Pause.SetActive(false);
        MainCanvas.SetActive(false);
        GameClear.SetActive(false);
        GameOver.SetActive(false);
        //Title.SetActive(true);

        Time.timeScale = 0;
    }

    //public void StartButton()
    //{
    //    //UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
    //    //UIObj.GetComponent<NextScene>().Init();
    //    SceneManager.LoadScene("Main");
    //}

    public void PauseButton_ON()
    {
        ButtonS.PlayOneShot(On);
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Pause);
    }
    public void PauseButton_OFF()
    {
        ButtonS.PlayOneShot(On);
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
    }

    public void RetryButton()
    {
        ButtonS.PlayOneShot(On);
        System.Threading.Thread.Sleep(1000);
        //SEが終わった後に処理
        UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Main);
        //UIObj.GetComponent<NextScene>().Init();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackTitleButton()
    {
        ButtonS.PlayOneShot(On);
        System.Threading.Thread.Sleep(1000);
        //UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.Title);
        //UIObj.GetComponent<NextScene>().Init();
        SceneManager.LoadScene("Title");
    }

    public void ExitButton()
    {
        ButtonS.PlayOneShot(On);
        System.Threading.Thread.Sleep(1000);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif // UNITY_EDITOR
    }

    public void MouseTop()
    {
        ButtonS.PlayOneShot(Top);
    }
}
