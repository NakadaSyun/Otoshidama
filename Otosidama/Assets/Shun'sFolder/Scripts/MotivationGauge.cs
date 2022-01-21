using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotivationGauge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider motivation;
    private bool PlayerStatus;

    private float volume;
    void Start()
    {
        volume = 0.5f;

        motivation.value = volume;
        PlayerStatus = GameObject.Find("Player").GetComponent<Player_ModeChange>().P_StudyMode;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerStatus = GameObject.Find("Player").GetComponent<Player_ModeChange>().P_StudyMode;
        Adjustment();
        motivation.value = volume;
        Debug.Log(volume);
    }

    void Adjustment()
    {
        if (PlayerStatus && volume > 0.0f)
        {
            //勉強中
            volume -= (1.0f / 20.0f) * Time.deltaTime;
        }
        else if(!PlayerStatus && volume < 1.0f)
        {
            //さぼり中
            volume += (1.0f / 15.0f) * Time.deltaTime;
        }
    }
}
