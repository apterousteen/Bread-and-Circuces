using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ListPlayers : MonoBehaviour
{
    [SerializeField] private Text PlayerName;
    [SerializeField] private Image ReadyColor;

    public void SetInfo()
    {
        PlayerName.text = PhotonNetwork.NickName;
    }
}