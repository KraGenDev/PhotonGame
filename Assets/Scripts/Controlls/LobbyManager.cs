using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _logText;
    
    
    void Start()
    {
        PhotonNetwork.NickName = "User " + Random.Range(0, 10000);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Log("User was connected to master");
    }

    public void CreateRoom()
    {
        var roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 5;
        PhotonNetwork.CreateRoom("bomj halupa", roomOptions);
    }

    public void JoinToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined to room");
        PhotonNetwork.LoadLevel(1);
    }

    private void Log(string text)
    {
        _logText.text += "\n" + text;
    }
}
