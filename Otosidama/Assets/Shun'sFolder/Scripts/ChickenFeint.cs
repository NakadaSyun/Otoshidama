using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeint : MonoBehaviour
{

    // Start is called before the first frame update
    private bool IsOnce;
    private float randSpeed;
    //SE
    public AudioClip Chicken;
    public AudioSource ChickenSource;
    private bool ChickenSfig;
    private float SETime;

    void Start()
    {
        transform.position = new Vector3(3.0f, 0.3f, -2.5f);
        IsOnce = false;
        ChickenSfig = false;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            ChickenSource.Pause();
        }
        else if (Time.timeScale != 0)
        {
            ChickenSource.UnPause();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.z < -2.5f)
        {
            if (IsOnce)
            {
                if (GameObject.Find("MainManager").GetComponent<MainManager>() != null)
                {
                    GameObject.Find("MainManager").GetComponent<MainManager>().chickenAttackStop();
                }
                else if (GameObject.Find("MainManager").GetComponent<OnlineMainManager>() != null)
                {
                    GameObject.Find("MainManager").GetComponent<OnlineMainManager>().chickenAttackStop();
                }
                IsOnce = false;
            }
            return;
        }
        MovePos();
    }

    void MovePos()
    {
        float sin = Mathf.Sin(Time.time * 5);

        SETime += Time.deltaTime;
        
        if(randSpeed > 7)
        {
            if (!ChickenSfig)
            {
                ChickenSource.PlayOneShot(Chicken);
                ChickenSfig = true;
            }else if(SETime > 2.5)
            {
                ChickenSource.Stop();
            }
            //下の２倍の速度で移動
            transform.position += new Vector3(0, (sin / 100),
                                (-2.4f * Time.deltaTime));
            Debug.Log("２倍だよ");
        }
        else
        {
            if (!ChickenSfig && SETime >2)
            {
                ChickenSource.PlayOneShot(Chicken);
                ChickenSfig = true;
            }
            //５秒で移動z軸を4～-2まで移動
            transform.position += new Vector3(0, (sin / 100),
                                (-1.2f * Time.deltaTime));
            Debug.Log("通常だよ");
        }
    }

    public void PosInit()
    {
        ChickenSfig = false;
        SETime = 0;
        transform.position = new Vector3(3.0f, 0.3f, 4.0f);
        IsOnce = true;
        randSpeed = 0;
        randSpeed = Random.Range(1, 10);
    }
}
