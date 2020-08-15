using UnityEngine;
using ZooClimber.Data;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class MovableCharacter : MonoBehaviour
    {
        const float DEFAULT_HIT_TIME = 3.0f;
        const float EXTRA_WIDTH_MARGIN = 0.1f;
        const float EXTRA_HEIGHT_MARGIN = 0.05f;
        
        public bool IsGrounded => isGrounded;
        [SerializeField] bool isGrounded;

        public bool IsBlocked => isBlocked;
        [SerializeField] bool isBlocked;

        public float Speed => formData.speed;

        public Rigidbody2D Rigidbody2D => rigidbody2d;
        Rigidbody2D rigidbody2d;

        public Collider2D Collider2D => collider2d;
        Collider2D collider2d;
        
        [SerializeField] float baseMoveSpeed = 100f;
        [SerializeField] float jumpForce = 300f;

        [SerializeField] protected FormData formData;

        [SerializeField] bool isHitCounting;
        [SerializeField] float hitCounter;
        
        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            collider2d = GetComponent<Collider2D>();
        }

        public void Move(float horizontalMove, bool isJumped, bool isHit, Vector3 hitSourcePos, float hitForce)
        {
            var raycastHit = Physics2D.Raycast(collider2d.bounds.center, Vector2.down, collider2d.bounds.extents.y + EXTRA_HEIGHT_MARGIN, GameManager.Instance.GroundMask);
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
            var forwardRaycastHit = Physics2D.Raycast(collider2d.bounds.center, moveDirection, collider2d.bounds.extents.x + EXTRA_WIDTH_MARGIN, GameManager.Instance.WallMask);
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

            if (isHit)
            {
                if (!isHitCounting)
                {
                    rigidbody2d.velocity = Vector2.zero;
                    
                    var pushDirection = Vector3.Normalize(transform.position - hitSourcePos) * hitForce;
                    rigidbody2d.AddForce(pushDirection, ForceMode2D.Impulse);
                    
                    isHitCounting = true;
                }
            }
            
            if (isHitCounting)
            {
                hitCounter += Time.deltaTime;
                
                if (hitCounter >= DEFAULT_HIT_TIME)
                {
                    hitCounter = 0f;
                    isHitCounting = false;
                }
            }

            var maxSpeed = baseMoveSpeed * formData.speed;
            if (Mathf.Abs(rigidbody2d.velocity.x) < maxSpeed)
            {
                rigidbody2d.AddForce(new Vector2(horizontalMove * maxSpeed * Time.deltaTime, rigidbody2d.velocity.y));
            }
            else
            {
                rigidbody2d.velocity = new Vector2(horizontalMove * maxSpeed * Time.deltaTime, rigidbody2d.velocity.y);
            }
        }
    }
}
