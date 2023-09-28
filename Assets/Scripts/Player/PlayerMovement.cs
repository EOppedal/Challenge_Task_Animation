using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float jumpSpeed = 12f;
        private Vector2 _desiredVelocity;

        [Header("CoyoteTime")] 
        public float coyoteTime = 0.2f;
        public float coyoteTimeCounter;

        [Header("JumpBuffer")] 
        public float jumpBufferTime = 0.2f;
        public float jumpBufferCounter;
    
        [Header("Acceleration")]
        public float accelerationTime = 0.02f;
        public float groundFriction = 0.03f;
        public float airFriction = 0.005f;
    
        [Header("Components")]
        private Rigidbody2D _rigidbody2D;
        private InputManager _input;
        private CollisionManager _collision;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _input = GetComponent<InputManager>();
            _collision = GetComponent<CollisionManager>();
        }

        private void Update()
        {
            _desiredVelocity = _rigidbody2D.velocity;
        
            _collision.Attack(_rigidbody2D, jumpSpeed);

            CoyoteTime();

            if (_input.jumpKey)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= 1 * Time.deltaTime;
            }
        
            // If we are eligible to Jump
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                _desiredVelocity.y = jumpSpeed; // Jump
                jumpBufferCounter = 0f;
            }
        
            if (!_input.jumpKey && _rigidbody2D.velocity.y > 0f)
            {
                _desiredVelocity.y *= 0.5f;
                coyoteTimeCounter = 0f;

            }
            _rigidbody2D.velocity = _desiredVelocity;
        }

        private void CoyoteTime()
        {
            if (_collision.IsPlayerGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= 1 * Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (_input.moveDirection.x != 0)
            {
                _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 
                    moveSpeed * _input.moveDirection.x, accelerationTime);
            }
            else
            {
                _desiredVelocity.x = Mathf.Lerp(_desiredVelocity.x, 0f, 
                    _collision.IsPlayerGrounded() ? groundFriction : airFriction);
            }

            _rigidbody2D.velocity = _desiredVelocity;
        }
    }
}