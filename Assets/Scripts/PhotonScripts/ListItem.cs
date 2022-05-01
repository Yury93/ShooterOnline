using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ListItem : MonoBehaviour
{
    [SerializeField] private Text roomNameTxt;
    [SerializeField] private Text countPlayerTxt;

    public void SetInfo(RoomInfo info)
    {
        roomNameTxt.text = info.Name;
        countPlayerTxt.text = info.PlayerCount.ToString() + "/" + info.MaxPlayers ;
    }
    public void JointListToRoom()
    {
        PhotonNetwork.JoinRoom(roomNameTxt.text);
    }
}
