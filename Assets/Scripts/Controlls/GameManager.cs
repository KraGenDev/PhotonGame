using System;
using System.Collections.Generic;
using Systems;
using DefaultNamespace.DTO;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private TextMeshProUGUI _textMesh;
        
        private void Awake()
        {
            RegisterCustomTypes();
        }

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

        private void RegisterCustomTypes()
        {
            PhotonPeer.RegisterType(typeof(UserData), 0, DataManipulator.SerializeUserData,
                DataManipulator.DeserializeUserData);
            
            PhotonPeer.RegisterType(typeof(List<UserData>), 1, DataManipulator.SerializeListToByteArray,
                DataManipulator.DeserializeListFromByteArray);
        }
        public void Leave()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
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
            
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.DestroyPlayerObjects(otherPlayer);
        }
    }
}