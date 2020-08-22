using UnityEngine;

namespace ZooClimber.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        public enum EnemyForm
        {
            None = -1,
            Snake = 0,
            Lion = 1,
            Eagle = 2,
            Cat = 3
        }

        public enum FaceDirection
        {
            Left = -1,
            Right = 1
        }

        [SerializeField] EnemyForm enemyForm;
        [SerializeField] SpriteRenderer formSprite;

        public Vector2 FaceDirectionToVector => faceDirection == FaceDirection.Left ? Vector2.left : Vector2.right;
        [SerializeField] protected FaceDirection faceDirection = FaceDirection.Right;
        
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
                if (CanFlip() && faceDirection == FaceDirection.Left && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    faceDirection = FaceDirection.Right;
                    formSprite.flipX = false;
                }
                else if (CanFlip() && faceDirection == FaceDirection.Right && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    faceDirection = FaceDirection.Left;
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
                movable.Move((float)faceDirection, false, false, Vector3.zero, 0f);
            }
        }
    }
}
