using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeCount : MonoBehaviour
{
    //カウントアップ
    public float countup = 0.0f;

    //タイムリミット
    public float timeLimit = 60.0f;

    //時間を表示するText
    [SerializeField] public Text timeText;

    GameObject UIObj;

    void Start()
    {
        UIObj = GameObject.Find("UI_Script");

        countup = timeLimit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //時間を表示する
        timeText.text = countup.ToString("f0");

        if(countup <= 0.0)
        {
            UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.GameClear);
        }
        else
        {
            if(timeText.IsActive() == true)
            {
                //時間をカウントする
                countup -= Time.deltaTime;
            }
        }
    }

    public void TimeReset()
    {
        countup = timeLimit;
    }
}
