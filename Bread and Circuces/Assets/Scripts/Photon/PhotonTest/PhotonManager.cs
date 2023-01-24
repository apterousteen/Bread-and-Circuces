using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;
    [SerializeField] private InputField roomName;
    [SerializeField] private ListItem itemPrefab;
    [SerializeField] private Transform content;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Вы подключены к: {PhotonNetwork.CloudRegion}");
        PhotonNetwork.JoinLobby();
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
        PhotonNetwork.LoadLevel("PhotonSceneTest");
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
            ListItem listItem = Instantiate(itemPrefab, content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
            }
        }
    }
}