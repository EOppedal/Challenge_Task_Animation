using UnityEngine;

namespace Player
{
   public class PlayerAnimation : MonoBehaviour
   {
      private Animator _animator;
      private InputManager _input;
      private CollisionManager _collision;
      private Rigidbody2D _rigidbody2D;
      private SpriteRenderer _spriteRenderer;

      private void Start()
      {
         _animator = GetComponent<Animator>();
         _input = GetComponent<InputManager>();
         _collision = GetComponent<CollisionManager>();
         _rigidbody2D = GetComponent<Rigidbody2D>();
         _spriteRenderer = GetComponent<SpriteRenderer>();
      }

      private void Update()
      {
         UpdateAnimation();
      }

      private void UpdateAnimation()
      {
         if (_collision.IsPlayerGrounded())_animator.Play(_input.moveDirection.x != 0 ? "Player_Walk" : "Player_Idle");
         else _animator.Play(_rigidbody2D.velocity.y > 0 ? "Player_Jump" : "Player_Fall");
         
         if (_input.moveDirection.x == 0) return;
         _spriteRenderer.flipX = !(_input.moveDirection.x > 0);
      }
   }
}