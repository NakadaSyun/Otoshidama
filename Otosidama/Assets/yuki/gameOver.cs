using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    GameObject mainManeger;
    GameObject UIObj;


    void Start()
    {
        mainManeger = GameObject.Find("MainManager");
        UIObj = GameObject.Find("UI_Script");
    }

    void Update()
    {
        if (mainManeger != null)
        {
            if (mainManeger.GetComponent<MainManager>() != null)
            {
                if (mainManeger.GetComponent<MainManager>().checkFind())
                {
                    UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.GameOver);
                    mainManeger.GetComponent<MainManager>().IsGameEnd = true;
                }
            }
            else if(mainManeger.GetComponent<OnlineMainManager>() != null)
            {
                if (mainManeger.GetComponent<OnlineMainManager>().checkFind())
                {
                    UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.GameOver);

                }
            }
            
        }
    }
}
