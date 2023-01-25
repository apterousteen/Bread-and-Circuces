using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class ListItem : MonoBehaviour
{
    [SerializeField] private Text textNameRoom;
    [SerializeField] private Text textPlayersCount;

    public void SetInfo(RoomInfo info)
    {
        textNameRoom.text = info.Name;
        textPlayersCount.text = $"{info.PlayerCount} / {info.MaxPlayers}";
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(textNameRoom.text);
    }
}