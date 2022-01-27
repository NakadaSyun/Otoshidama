using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonViewText : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    private InputField field;

    public string _text;
    public bool _IsGameStart;

    public bool IsGameStart
    {
        get { return _IsGameStart; }
        set { _IsGameStart = value; RequestOwner(); }
    }

    void Awake()
    {
        this.photonView = GetComponent<PhotonView>();
        _IsGameStart = false;
    }

    public void Update()
    {
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {
            stream.SendNext(IsGameStart);
        }
        // オーナー以外の場合
        else
        {
            IsGameStart = (bool)stream.ReceiveNext();
        }
    }

    private void RequestOwner()
    {
        if (this.photonView.IsMine == false)
        {
            if (this.photonView.OwnershipTransfer != OwnershipOption.Request)
                Debug.LogError("OwnershipTransferをRequestに変更してください。");
            else
                this.photonView.RequestOwnership();
        }
    }
}