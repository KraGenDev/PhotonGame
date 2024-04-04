using Photon.Pun;
using UnityEngine;

namespace Character
{
    public class CharacterWeapon : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _shootForce;
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire1") && _photonView.IsMine)
            {
                var bullet = PhotonNetwork.Instantiate(_bulletPrefab.name, _firePoint.position, _firePoint.rotation);
                var rigidbody = bullet.GetComponent<Rigidbody>();
                
                rigidbody.velocity =_firePoint.forward * (_shootForce * Time.deltaTime);
            }
        }
    }
}