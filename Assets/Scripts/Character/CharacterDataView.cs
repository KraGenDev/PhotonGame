using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using DefaultNamespace;
using DefaultNamespace.DTO;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class CharacterDataView : MonoBehaviour,IOnEventCallback,IDamageable
    {
        [SerializeField] private TextMeshPro _nickName;
        [SerializeField] private TextMeshPro _health;
        [SerializeField] private PhotonView _photonView;

        private UserData _data;

        private void Start()
        {
            _data = new UserData
            {
                NickName = _photonView.Owner.NickName, 
                Health = 100,
                ID = _photonView.Owner.ActorNumber
            };
            
             SetData(_data);
             var dataManager = FindObjectOfType<GameUsersDataManager>();
             dataManager?.AddNewUserView(_data);
        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        private void SetData(UserData data)
        {
            _nickName.text = data.NickName;
            _health.text = data.Health.ToString();
        }

        private void UpdateUI()
        {
            if (_data == null) return;
            
            _nickName.text = _data.NickName;
            _health.text = _data.Health.ToString();

            PhotonNetwork.RaiseEvent(3, _data, new RaiseEventOptions() {Receivers = ReceiverGroup.Others},
                new SendOptions() {Reliability = true});
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case 2:
                    var data =(List<UserData>) photonEvent.CustomData;
                    var user = data.FirstOrDefault(item => item.ID == _data.ID);
                    
                    if (user != null)
                    {
                        Debug.Log(user.NickName,gameObject);
                        SetData(user);
                    }
                    break;
                case 3:
                    var userData =(UserData) photonEvent.CustomData;
                    if (userData.ID == _data.ID)
                    {
                        Debug.Log(userData.NickName + "|" + userData.Health,gameObject);
                        SetData(userData);
                    }
                    break;
            }
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("call take damage");
            if (!_photonView.IsMine) return;
            Debug.Log("damaged");
            _data.Health -= damage;
            UpdateUI();
        }
    }
}