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
        
        protected MovableCharacter character;

        bool isPlayer;
        bool isEnemy;
        
        void Awake()
        {
            character = GetComponent<MovableCharacter>();
            isPlayer = character is PlayerCharacter;
            isEnemy = character is EnemyCharacter;
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
            if (CanFlip() && moveDirection == MoveDirection.Left && character.IsGrounded)
            {
                character.Rigidbody2D.velocity = Vector2.zero;
                moveDirection = MoveDirection.Right;
                formSprite.flipX = false;
            }
            else if (CanFlip() && moveDirection == MoveDirection.Right && character.IsGrounded)
            {
                character.Rigidbody2D.velocity = Vector2.zero;
                moveDirection = MoveDirection.Left;
                formSprite.flipX = true;
            }
        }

        bool CanFlip()
        {
            if (isPlayer)
            {
                return character.IsBlocked;
            }

            if (isEnemy)
            {
                return character.IsBlocked || !character.IsNextGroundExpected;
            }

            return false;
        }
        
        protected virtual void UpdateCharacter()
        {
            character.Move((float)moveDirection, false, false, Vector3.zero, 0f);
        }
    }
}
