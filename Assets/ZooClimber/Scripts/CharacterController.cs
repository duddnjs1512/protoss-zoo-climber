using UnityEngine;

namespace ZooClimber.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        protected enum MoveDirection
        {
            Left = -1,
            Right = 1
        }
        
        [SerializeField] SpriteRenderer formSprite;
        [SerializeField] protected MoveDirection moveDirection = MoveDirection.Right;
        
        protected MovableCharacter movable;

        bool isPlayer;
        bool isEnemy;
        
        void Awake()
        {
            movable = GetComponent<MovableCharacter>();
            isPlayer = movable is PlayerCharacter;
            isEnemy = !isPlayer;
        }
        
        void Update()
        {
            UpdateController();
        }

        void FixedUpdate()
        {
            UpdateCharacter();
        }

        protected virtual void UpdateController()
        {
            if (movable)
            {
                if (CanFlip() && moveDirection == MoveDirection.Left && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    moveDirection = MoveDirection.Right;
                    formSprite.flipX = false;
                }
                else if (CanFlip() && moveDirection == MoveDirection.Right && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    moveDirection = MoveDirection.Left;
                    formSprite.flipX = true;
                }
            }
        }

        bool CanFlip()
        {
            if (isPlayer)
            {
                return movable.IsBlocked;
            }

            if (isEnemy)
            {
                return movable.IsBlocked || !movable.IsNextGroundExpected;
            }

            return false;
        }
        
        protected virtual void UpdateCharacter()
        {
            if (movable)
            {
                movable.Move((float)moveDirection, false, false, Vector3.zero, 0f);
            }
        }
    }
}
