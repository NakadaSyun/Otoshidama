using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsMove : MonoBehaviour
{
    [SerializeField] GameObject friends;
    public float span;
    private float activeTime,currentTime;
    public bool Init;
    private bool IsOnce;

    public void Start()
    {
        Transform myTransform = friends.transform;

        Vector3 pos = myTransform.position;
        pos.x = 2.2f;
        pos.y = 0.2f;
        pos.z = -2f;
        myTransform.position = pos;

        span = 2f;
        currentTime = 0f;
        activeTime = 0f;
        Init = false;
        IsOnce = false;
    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if (true)
        {
            Transform myTransform = friends.transform;

            if (myTransform.position.z > -0.5f)
            {
                Vector3 pos = myTransform.position;
                pos.z -= (2.75f / 2) * Time.deltaTime;
                myTransform.position = pos;

            }
            else if (myTransform.position.z < -0.5f && myTransform.position.z > -2.0f)        //立ち止まる
            {
                GameObject.Find("MainManager").GetComponent<MainManager>().canFriendFind = true;
                activeTime += Time.deltaTime;
            }

            if(activeTime > span && myTransform.position.z > -2f)       //移動再開
            {
                GameObject.Find("MainManager").GetComponent<MainManager>().canFriendFind = false;
                Vector3 pos = myTransform.position;
                pos.z -= 1.25f * Time.deltaTime;
                myTransform.position = pos;
            }

            if (myTransform.position.z <= -2f)
            {
                if (IsOnce)
                {
                    GameObject.Find("MainManager").GetComponent<MainManager>().chickenAttackStop();
                    IsOnce = false;
                }
                if (Init)
                {
                    currentTime = 0;
                    activeTime = 0;

                    Vector3 pos = myTransform.position;
                    pos.z = 2f;
                    myTransform.position = pos;
                    Init = false; IsOnce = true;
                }
            }
        }
        //}
        //if(activeTime > 0f)
        //{
        //    friends.SetActive(true);
        //    activeTime -= Time.deltaTime;
        //    if (activeTime < 0f)
        //    {
        //        activeTime = 0f;
        //        currentTime = 0f;
        //        friends.SetActive(false);
        //    }
        //}

        //myTransform.position = pos;
    }
}
