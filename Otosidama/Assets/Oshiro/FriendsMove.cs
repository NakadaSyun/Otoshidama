using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsMove : MonoBehaviour
{
    [SerializeField] GameObject friends;
    public float span;
    private float activeTime,currentTime;

    private void Start()
    {
        Transform myTransform = friends.transform;

        Vector3 pos = myTransform.position;
        pos.x = 2f;
        pos.y = 0.2f;
        pos.z = 2f;
        myTransform.position = pos;

        span = 3f;
        currentTime = 0f;
        activeTime = 0f;
    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            Debug.LogFormat("{0}秒経過", span);
            Transform myTransform = friends.transform;

            if (myTransform.position.z > -0.5f && activeTime < span)
            {
                Vector3 pos = myTransform.position;
                pos.z -= 0.005f;
                myTransform.position = pos;

            }
            else if (myTransform.position.z < -0.5f)
            {
                activeTime += Time.deltaTime;
            }

            if(activeTime > span)
            {
                Vector3 pos = myTransform.position;
                pos.z -= 0.005f;
                myTransform.position = pos;
            }

            if (myTransform.position.z < -2f)
            {
                currentTime = 0;
                activeTime = 0;

                Vector3 pos = myTransform.position;
                pos.z = 2f;
                myTransform.position = pos;
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
        Debug.Log(activeTime);

        //myTransform.position = pos;
    }
}
