using System;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;

namespace Character
{
    public class CharacterCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Camera _camera;
        
        private IInput _input;
        private float X, Y;

        private void Start()
        {
            _camera.enabled = _photonView.IsMine;
        }

        private void Update()
        {
            if (!_photonView.IsMine)
                return;
            
            CameraRotate();
        }

        private void CameraRotate()
        {
            if (!Cursor.visible)
            {
                var player = transform.parent.gameObject;
                X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed;
                Y += Input.GetAxis("Mouse Y") * rotateSpeed;
                Y = Mathf.Clamp(Y, minAngle, maxAngle);
                transform.localEulerAngles = new Vector3(-Y, transform.localEulerAngles.y, 0);
                player.transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x,
                    player.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed,
                    player.transform.localEulerAngles.z);
            }
        }
    }
}