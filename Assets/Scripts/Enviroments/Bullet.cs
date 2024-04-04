using System;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace.Enviroments
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private TrailRenderer _trail;

        private void OnEnable()
        {
            _trail.Clear();
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other.gameObject.name,other.gameObject);
            
            if (other.gameObject.TryGetComponent<IDamageable>(out var iDamageable))
            {
                iDamageable.TakeDamage(_damage);
            }
            
            Destroy(gameObject);
        }
    }
}