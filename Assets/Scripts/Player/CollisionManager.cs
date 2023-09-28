using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class CollisionManager : MonoBehaviour
    {
        [Header("isGrounded")]
        public LayerMask whatIsGround;
        private float distanceToGround = 1f;
    
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DeathZone"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (other.CompareTag("Goal"))
            {
                SceneManager.LoadScene("Win");
            }
        }
    
        public bool IsPlayerGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, whatIsGround);
        }

        public void Attack(Rigidbody2D _rb2D, float JumpSpeed)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
            if (hit.collider == null) return;
            if (!hit.transform.GetComponent<CapsuleCollider2D>()) return;

            if (hit.transform.CompareTag("Enemy"))
            {
                Destroy(hit.transform.gameObject);
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, JumpSpeed / 2);
            }
        }
    }
}