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
        [SerializeField] bool isJumpButtonClicked;
        [SerializeField] bool isTransformClicked;
        [SerializeField] bool isFreeMove;

        void Awake()
        {
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        void Update()
        {
            if (!isJumpButtonClicked)
            {
                isJumpButtonClicked = Input.GetButtonDown("Jump");
            }
            
            if (!isTransformClicked)
            {
                isTransformClicked = Input.GetButtonDown("Transform");
            }

            if (transform.position.x < minXPos + playerCharacter.Collider2D.bounds.extents.x ||
                playerCharacter.IsBlocked && moveDirection == MoveDirection.Left && playerCharacter.IsGrounded)
            {
                moveDirection = MoveDirection.Right;
                formSprite.flipX = false;
            }
            else if (transform.position.x > maxXPos - playerCharacter.Collider2D.bounds.extents.x ||
                     playerCharacter.IsBlocked && moveDirection == MoveDirection.Right && playerCharacter.IsGrounded)
            {
                moveDirection = MoveDirection.Left;
                formSprite.flipX = true;
            }
        }

        void FixedUpdate()
        {
            var horizontalMove = isFreeMove ? Input.GetAxis("Horizontal") : (float)moveDirection;
            playerCharacter.Move(horizontalMove, isJumpButtonClicked);
            isJumpButtonClicked = false;

            if (isTransformClicked)
            {
                playerCharacter.RandomTransform();
                isTransformClicked = false;
            }
        }
    }
}
