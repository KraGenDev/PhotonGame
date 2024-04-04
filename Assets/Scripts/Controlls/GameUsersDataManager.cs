using System.Collections.Generic;
using DefaultNamespace.DTO;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class GameUsersDataManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public List<UserData> UsersData;

        public void AddNewUserView(UserData data)
        {
            if(UsersData != null)
                _text.text = UsersData.Count.ToString();
            
            if (!PhotonNetwork.IsMasterClient) 
                return;
            
            UsersData ??= new List<UserData>();

            UsersData.Add(data);

            PhotonNetwork.RaiseEvent(2, UsersData, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
        }
    }
}