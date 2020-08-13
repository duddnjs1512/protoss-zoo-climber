using UnityEngine;

namespace ZooClimber.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        public float minXPos = -20f;
        public float maxXPos = 20f;
        
        protected enum MoveDirection
        {
            Left = -1,
            Right = 1
        }
        
        [SerializeField] SpriteRenderer formSprite;
        [SerializeField] protected MoveDirection moveDirection = MoveDirection.Right;
        
        protected MovableCharacter character;
        
        void Awake()
        {
            character = GetComponent<MovableCharacter>();
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
            if (transform.position.x < minXPos + character.Collider2D.bounds.extents.x ||
                character.IsBlocked && moveDirection == MoveDirection.Left && character.IsGrounded)
            {
                moveDirection = MoveDirection.Right;
                formSprite.flipX = false;
            }
            else if (transform.position.x > maxXPos - character.Collider2D.bounds.extents.x ||
                     character.IsBlocked && moveDirection == MoveDirection.Right && character.IsGrounded)
            {
                moveDirection = MoveDirection.Left;
                formSprite.flipX = true;
            }
        }
        
        protected virtual void UpdateCharacter()
        {
            character.Move((float)moveDirection, false);
        }
    }
}
