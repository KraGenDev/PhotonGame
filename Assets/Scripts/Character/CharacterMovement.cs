using Photon.Pun;
using UnityEngine;

namespace Character
{
    public class CharacterMovement : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private float speed = 6.0F;
        [SerializeField] private float gravityForce = 20.0F;

        public float jumpSpeed = 2.0f;
        public float gravity = 10.0f;
        
        private CharacterController _characterController;
        private PhotonView _photonView;
        private bool _isActive;
        private float _xRotation = 0f;
        private Vector3 moveDirection = Vector3.zero;

        private Vector3 movingDirection = Vector3.zero;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_isActive);
            }
            else
            {
                _isActive =(bool) stream.ReceiveNext();
            }
        }

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!_photonView.IsMine)
            {
                _gameObject.SetActive(_isActive);
                return;
            }

            if (Input.GetKeyDown(KeyCode.K))
                Cursor.visible = !Cursor.visible;

            if (_characterController.isGrounded) {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                
                moveDirection *= speed;

                if (Input.GetButtonDown("Jump")) 
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravityForce * Time.deltaTime;
            _characterController.Move(moveDirection * Time.deltaTime);
            
            _isActive = Input.GetKey(KeyCode.Mouse0);
            _gameObject.SetActive(_isActive);
        }
    }
}

