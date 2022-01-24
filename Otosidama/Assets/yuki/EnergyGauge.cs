using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGauge : MonoBehaviour
{
    [SerializeField] Slider gameGauge;
    [SerializeField] Slider stadyGauge;

    GameObject UIObj;

    [SerializeField] float speed = 50f;

    bool playerStatus;
    void Start()
    {
        UIObj = GameObject.Find("UI_Script");

        playerStatus = false;
    }

    void Update()
    {
        playerStatus = GameObject.Find("Player").GetComponent<Player_ModeChange>().P_StudyMode;

        if (playerStatus == true)
        {
            stadyGauge.value += Time.deltaTime / speed;
        }
        else
        {
            gameGauge.value += Time.deltaTime / speed;
        }

        if(stadyGauge.value >= 1f && gameGauge.value >= 1f)
        {
            if (UIObj != null)
            {
                UIObj.GetComponent<NextScene>().SceneChange(NextScene.Scene.GameClear);
            }
        }
    }
}
