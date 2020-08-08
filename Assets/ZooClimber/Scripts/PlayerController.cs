using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof (PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        public float minXPos = -20f;
        public float maxXPos = 20f;
        
        enum MoveDirection
        {
            Left = -1,
            Right = 1
        }
        
        [SerializeField] PlayerCharacter playerCharacter;

        [SerializeField] SpriteRenderer formSprite;
        [SerializeField] MoveDirection moveDirection = MoveDirection.Right; 
        [SerializeField] bool isJumped;
        [SerializeField] bool isTransformed;
        [SerializeField] bool isFreeMove = false;

        void Awake()
        {
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        void Update()
        {
            if (!isJumped)
            {
                isJumped = Input.GetButtonDown("Jump");
            }

            if (!isTransformed)
            {
                isTransformed = Input.GetButtonDown("Transform");
            }

            if (transform.position.x < minXPos + playerCharacter.Collider2D.bounds.extents.x ||
                playerCharacter.IsBlocked && moveDirection == MoveDirection.Left)
            {
                moveDirection = MoveDirection.Right;
                formSprite.flipX = false;
            }
            else if (transform.position.x > maxXPos - playerCharacter.Collider2D.bounds.extents.x ||
                     playerCharacter.IsBlocked && moveDirection == MoveDirection.Right)
            {
                moveDirection = MoveDirection.Left;
                formSprite.flipX = true;
            }
        }

        void FixedUpdate()
        {
            var horizontalMove = isFreeMove ? Input.GetAxis("Horizontal") : (float)moveDirection;
            playerCharacter.Move(horizontalMove, isJumped);
            isJumped = false;
        }
    }
}
