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
    //SE
    public AudioClip friendsWalk;
    public AudioSource friendsS;
    public bool friendsSfig;

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
        friendsSfig = false;
    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if (Time.timeScale == 0)
        {
            friendsS.Pause();
        }
        else if (Time.timeScale != 0)
        {
            friendsS.UnPause();
        }

        if (true)
        {
            Transform myTransform = friends.transform;

            if (myTransform.position.z > -0.5f)
            {
                if (!friendsSfig)
                {
                    friendsS.PlayOneShot(friendsWalk);
                    friendsSfig = true;
                }
                Vector3 pos = myTransform.position;
                pos.z -= (2.75f / 2) * Time.deltaTime;
                myTransform.position = pos;

            }
            else if (myTransform.position.z < -0.5f && myTransform.position.z > -2.0f)        //立ち止まる
            {
                if (activeTime < span)
                {
                    friendsS.Stop();
                    friendsSfig = false;
                }
                GameObject.Find("MainManager").GetComponent<MainManager>().canFriendFind = true;
                activeTime += Time.deltaTime;
            }

            if(activeTime > span && myTransform.position.z > -2f)       //移動再開
            {
                if (!friendsSfig)
                {
                    Debug.Log("再開");
                    friendsS.PlayOneShot(friendsWalk);
                    friendsSfig = true;
                }
                if (GameObject.Find("MainManager").GetComponent<MainManager>() != null)
                {
                    GameObject.Find("MainManager").GetComponent<MainManager>().canFriendFind = false;
                }
                else if (GameObject.Find("MainManager").GetComponent<OnlineMainManager>() != null)
                {
                    GameObject.Find("MainManager").GetComponent<OnlineMainManager>().canFriendFind = false;
                }
                    Vector3 pos = myTransform.position;
                pos.z -= 1.25f * Time.deltaTime;
                myTransform.position = pos;
            }

            if (myTransform.position.z <= -2f)
            {
                if (IsOnce)
                {
                    friendsS.Stop();
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
                if (Init)
                {
                    currentTime = 0;
                    activeTime = 0;

                    Vector3 pos = myTransform.position;
                    pos.z = 2f;
                    myTransform.position = pos;
                    Init = false; IsOnce = true;
                    friendsSfig = false;
                }
            }
        }
    }
}
