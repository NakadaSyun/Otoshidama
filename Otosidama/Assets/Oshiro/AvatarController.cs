using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float Maxstady = 6f;
    [SerializeField] private Image gameGauge = default;
    [SerializeField] private Image stadyGauge = default;

    private float currentstady = 0f;

    GameObject UIObj;

    [SerializeField] float speed = 50f;

    bool playerStatus;

    private void Start()
    {
        UIObj = GameObject.Find("UI_Script");

        playerStatus = false;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            //playerStatus = GameObject.Find("Player").GetComponent<Player_ModeChange>().P_StudyMode;
            playerStatus = true;

            if (playerStatus == true)
            {
                currentstady = Mathf.Max(0f,currentstady + Time.deltaTime);
            }
            if(currentstady > Maxstady)
            {
                currentstady = 0f;
            }
        }
        //Debug.Log(currentstady);
        // スタミナをゲージに反映する
        stadyGauge.fillAmount = currentstady / Maxstady;
        Debug.Log(stadyGauge.fillAmount);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentstady);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            currentstady = (float)stream.ReceiveNext();
        }
    }
}