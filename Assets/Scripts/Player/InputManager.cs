using UnityEngine;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public bool jumpKey;

        public Vector2 moveDirection;

        private void Update()
        {
            moveDirection.x = Input.GetAxis("Horizontal");
            moveDirection.y = Input.GetAxis("Vertical");

            jumpKey = Input.GetAxis("Jump") != 0;
        }
    }
}