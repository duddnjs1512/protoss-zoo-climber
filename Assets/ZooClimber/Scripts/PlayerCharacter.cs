using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCharacter : MonoBehaviour
    {
        const float EXTRA_WIDTH_MARGIN = 0.1f;
        const float EXTRA_HEIGHT_MARGIN = 0.05f;

        [SerializeField] float maxSpeed = 10f;
        [SerializeField] float jumpForce = 400f;
        [SerializeField] float groundCheckDistance = 5;
        [SerializeField] LayerMask groundMask;
        [SerializeField] bool isGrounded;

        public bool IsBlocked => isBlocked;
        [SerializeField] bool isBlocked;

        public Rigidbody2D Rigidbody2D => rigidbody2d;
        Rigidbody2D rigidbody2d;

        public Collider2D Collider2D => collider2d;
        Collider2D collider2d;
        
        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            collider2d = GetComponent<Collider2D>();
        }

        public void Move(float horizontalMove, bool isJumped)
        {
            var raycastHit = Physics2D.Raycast(collider2d.bounds.center, Vector2.down, collider2d.bounds.extents.y + EXTRA_HEIGHT_MARGIN, groundMask);
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
            Debug.DrawRay(collider2d.bounds.center, Vector2.down * (collider2d.bounds.extents.y + EXTRA_HEIGHT_MARGIN), rayColor);

            var moveDirection = new Vector2(horizontalMove, 0f);
            var forwardRaycastHit = Physics2D.Raycast(collider2d.bounds.center, moveDirection, collider2d.bounds.extents.x + EXTRA_HEIGHT_MARGIN, groundMask);
            Color forwardRayColor;
            if (forwardRaycastHit.collider != null)
            {
                forwardRayColor = Color.green;
                isBlocked = true;
            }
            else
            {
                forwardRayColor = Color.red;
                isBlocked = false;
            }
            Debug.DrawRay(collider2d.bounds.center, moveDirection * (collider2d.bounds.extents.x + EXTRA_WIDTH_MARGIN), forwardRayColor);
            
            if (isGrounded && isJumped)
            {
                isGrounded = false;
                rigidbody2d.AddForce(new Vector2(0f, jumpForce));
            }
            
            rigidbody2d.velocity = new Vector2(horizontalMove * maxSpeed * Time.deltaTime, rigidbody2d.velocity.y);
        }
    }
}
