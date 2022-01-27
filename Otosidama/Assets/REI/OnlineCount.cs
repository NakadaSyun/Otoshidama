using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class OnlineCount : MonoBehaviour
{
    GameObject MatchMakingobj;

    Matching matchingScript;

    public Text textobj;


    // Start is called before the first frame update
    void Start()
    {
        MatchMakingobj = GameObject.Find("UI_Script");
        matchingScript = MatchMakingobj.GetComponent<Matching>();
    }

    // Update is called once per frame
    void Update()
    {

        textobj.text = "接続人数" + PhotonNetwork.CountOfPlayersInRooms;

    }
}
