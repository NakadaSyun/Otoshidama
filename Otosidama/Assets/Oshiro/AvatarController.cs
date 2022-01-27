using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float Maxstady = 6f;
    private const float Maxgame = 6f;
    [SerializeField] private Image gameGauge = default;
    [SerializeField] private Image stadyGauge = default;

    private float currentStady = 0f;
    private float currentGame = 0f;

    GameObject UIObj;

    [SerializeField] float speed = 5f;

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
            if (this.GetComponent<Player_ModeChange>() != null)
            {
                playerStatus = this.GetComponent<Player_ModeChange>().P_StudyMode;
            }
            else if (this.GetComponent<OnlinePlayer_ModeChange>() != null)
            {
                playerStatus = this.GetComponent<OnlinePlayer_ModeChange>().P_StudyMode;
            }

            if (playerStatus == true)
            {
                currentStady = Mathf.Max(0f, currentStady + Time.deltaTime);
            }
            else
            {
                currentGame = Mathf.Max(0f, currentGame + Time.deltaTime);
            }
            if (currentStady > Maxstady)
            {
                currentStady = 0f;
            }
            if (currentGame > Maxgame)
            {
                currentGame = 0f;
            }
        }
        //Debug.Log(currentstady);
        // スタミナをゲージに反映する
        stadyGauge.fillAmount = currentStady / Maxstady;
        gameGauge.fillAmount = currentGame / Maxgame;
        Debug.Log(gameGauge.fillAmount);
        Debug.Log(stadyGauge.fillAmount);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(currentStady);
            stream.SendNext(currentGame);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            currentStady = (float)stream.ReceiveNext();
            currentGame = (float)stream.ReceiveNext();
        }
    }
}