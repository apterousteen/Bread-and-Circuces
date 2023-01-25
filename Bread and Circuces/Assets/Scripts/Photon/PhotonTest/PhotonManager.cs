using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;
    [SerializeField] private string nickName;
    [SerializeField] private InputField roomName; 
    [SerializeField] private ListItem itemPrefab;
    [SerializeField] private Transform content;

    private List<RoomInfo> allRoomsInfo = new List<RoomInfo>();

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Вы подключены к: {PhotonNetwork.CloudRegion}");
        if (nickName == "")
        {
            PhotonNetwork.NickName = "User" + Random.Range(0,100);
        }
        else
            PhotonNetwork.NickName = nickName;

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Вы отключены от сервера!");
    }

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName.text, roomOptions, typedLobby: default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Создана комната, имя комнаты: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Не удалось создать комнату!");
    }

    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if (allRoomsInfo[i].masterClientId == info.masterClientId)
                    return;
            }

            ListItem listItem = Instantiate(itemPrefab, content);

            if (listItem != null)
            {
                listItem.SetInfo(info);
                allRoomsInfo.Add(info);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("choiceMenu");
    }

    public void JoinRandRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void LeaveButton()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("PhotonTest");
    }
}