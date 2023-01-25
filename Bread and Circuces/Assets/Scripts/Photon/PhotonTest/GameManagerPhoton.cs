using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class GameManagerPhoton : MonoBehaviour
{
    [SerializeField] private Text textLastMesssage;
    [SerializeField] private InputField textMesssageField;

    private PhotonView PhotonView;

    private void Start()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    public void SendButton()
    {
        PhotonView.RPC("Send_Data", RpcTarget.AllBuffered, PhotonNetwork.NickName, textMesssageField.text);
    }
    
    [PunRPC]
    private void Send_Data(string nick, string message)
    {
        textLastMesssage.text = nick + ": " + message;
    }
}