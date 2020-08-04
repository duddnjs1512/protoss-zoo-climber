using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerCharacter : MonoBehaviour
    {
        const float EXTRA_HEIGHT_MARGIN = 0.05f;

        [SerializeField] float maxSpeed = 10f;
        [SerializeField] float jumpForce = 400f;
        [SerializeField] float groundCheckDistance = 5;
        [SerializeField] LayerMask groundMask;
        [SerializeField] bool isGrounded;

        public Rigidbody2D Rigidbody2D => rigidbody2d;
        Rigidbody2D rigidbody2d;

        public BoxCollider2D BoxCollider2D => boxCollider2d;
        BoxCollider2D boxCollider2d;
        
        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            boxCollider2d = GetComponent<BoxCollider2D>();
        }

        public void Move(float horizontalMove, bool isJumped)
        {
            var raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + EXTRA_HEIGHT_MARGIN, groundMask);
            Color rayColor;
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
                isGrounded = true;
            }
            else
            {
                rayColor = Color.red;
            }
            Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + EXTRA_HEIGHT_MARGIN), rayColor);

            if (isGrounded && isJumped)
            {
                isGrounded = false;
                rigidbody2d.AddForce(new Vector2(0f, jumpForce));
            }
            
            rigidbody2d.velocity = new Vector2(horizontalMove * maxSpeed * Time.deltaTime, rigidbody2d.velocity.y);
        }
    }
}
