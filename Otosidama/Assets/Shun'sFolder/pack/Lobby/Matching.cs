using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Matching : MonoBehaviourPunCallbacks
{
    [SerializeField] private string gameVersion = "0.1";
    [SerializeField] public string nickName = null;
    [SerializeField] private string roomName = "TestRoom";

    [SerializeField] private Button MatchButton;

    [SerializeField] private GameObject MatBefor;
    [SerializeField] private GameObject Matnow;

    //InputFieldを格納するための変数
    InputField inputField;
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.NickName = nickName;
    }
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find("NameInput").GetComponent<InputField>();
        inputField.text = null;
        MatchButton.interactable = false;


        MatBefor.SetActive(true);     //名前入力のUIを表示
        Matnow.SetActive(false);      //GAMESTARTのUIを消去
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            MatchButton.interactable = true;
        }
        else
        {
            MatchButton.interactable = false;
        }
    }
    public void MatchingButton()
    {
        //InputFieldからテキスト情報を取得する
        nickName = inputField.text;
        if (string.IsNullOrEmpty(nickName))
        {
            //何も入力されていなければ処理をしない
            return;
        }

        PhotonNetwork.NickName = nickName;

        //入力フォームのテキストを空にする
        //inputField.text = "";\
        // イベントに登録
        //SceneManager.sceneLoaded += GameSceneLoaded;


        Connect();

    }

    public void GameStartButton()
    {
        // シーン切り替え
        SceneManager.LoadScene("OnlineMain");
    }

    private void Connect()
    {
        //Debug.Log("Photon Cloud に接続します。");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void JoinRoom()
    {
        //Debug.Log($"{roomName}に参加します。");
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("Photon Cloud に接続しました。");
        JoinRoom();
    }

    public override void OnJoinedRoom()
    {
        //Debug.Log($"{roomName} に参加しました。");

        MatBefor.SetActive(false);     //名前入力のUIを消去
        Matnow.SetActive(true);      //GAMESTARTのUIを表示

        foreach (var player in PhotonNetwork.PlayerList)
        {
            Debug.Log($"{player.NickName}({player.ActorNumber})");
            GameObject.Find("Text").GetComponent<Text>().text = ($"{player.NickName}({player.ActorNumber})");
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //Debug.Log($"{newPlayer.NickName} が入室しました。");
    }
}
