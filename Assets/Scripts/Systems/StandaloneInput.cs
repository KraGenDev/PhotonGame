using System;
using DefaultNamespace;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Systems
{
    public class StandaloneInput : MonoBehaviour,IInput
    {
        public event Action<Vector2> MoveDirection;
        public event Action Jump;

        private void Update()
        {
            var direction = Vector2.Zero;

            direction.X = Input.GetAxis("Horizontal");
            direction.Y = Input.GetAxis("Vertical");
            
            if(Input.GetKeyDown(KeyCode.Space))
                Jump?.Invoke();
            
            MoveDirection?.Invoke(direction);
        }
    }
}