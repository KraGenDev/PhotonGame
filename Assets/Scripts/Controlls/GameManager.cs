using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Controlls
{
    public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity, 0);
            }
        }

        public override void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public override void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void Leave()
        {
            Debug.Log("click");
            
            PhotonNetwork.RaiseEvent(1, "bober livaet", new RaiseEventOptions() {Receivers = ReceiverGroup.Others},
                new SendOptions() {Reliability = true});
            
            //PhotonNetwork.LeaveRoom();
        }
        
        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("New player joined: " + newPlayer.NickName);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left: " + otherPlayer.NickName);
            
            PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        }

        private void Log(string text)
        {
            _textMesh.text += "\n" + text;
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case 1:
                    Debug.Log((string)photonEvent.CustomData);
                    break;
            }
        }
    }
}