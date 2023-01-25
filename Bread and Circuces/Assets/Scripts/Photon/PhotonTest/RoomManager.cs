using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private ListPlayers PlayerPrefab;
    [SerializeField] private Transform content;

    private List<ListPlayers> _listings = new List<ListPlayers>();
    private void Start()
    {
        
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        ListPlayers listPlayers = Instantiate(PlayerPrefab, content);
        if (listPlayers != null)
        {
            listPlayers.SetInfo();
            _listings.Add(listPlayers);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
}
