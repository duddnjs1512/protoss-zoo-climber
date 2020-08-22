using UnityEngine;

namespace ZooClimber.Scripts
{
    public class CharacterController : MonoBehaviour
    {
        const float FLIP_INTERVAL = 1f;
        
        protected enum FaceDirection
        {
            Left = -1,
            Right = 1
        }

        public Vector2 FaceDirectionToVector => faceDirection == FaceDirection.Left ? Vector2.left : Vector2.right;
        [SerializeField] protected FaceDirection faceDirection = FaceDirection.Right;
        
        protected MovableCharacter movable;
        SpriteRenderer formSprite;

        bool isPlayer;
        bool isEnemy;

        [SerializeField] float flipCounter;
        [SerializeField] bool isFlipCounting;
        
        void Awake()
        {
            movable = GetComponent<MovableCharacter>();
            formSprite = GetComponent<SpriteRenderer>();
            
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
            Debug.Log(flipCounter);

            if (movable)
            {
                if (isFlipCounting)
                {
                    flipCounter += Time.deltaTime;
                }

                if (isFlipCounting && flipCounter >= FLIP_INTERVAL)
                {
                    isFlipCounting = false;
                    flipCounter = 0;
                }

                if (CanFlip() && faceDirection == FaceDirection.Left && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    
                    OnFlip();

                    faceDirection = FaceDirection.Right;
                    formSprite.flipX = false;
                }
                else if (CanFlip() && faceDirection == FaceDirection.Right && movable.IsGrounded)
                {
                    movable.Rigidbody2D.velocity = Vector2.zero;
                    
                    OnFlip();

                    faceDirection = FaceDirection.Left;
                    formSprite.flipX = true;
                }
            }
        }

        bool CanFlip()
        {
            if (isFlipCounting)
            {
                return false;
            }
            
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

        protected virtual void OnFlip()
        {
            flipCounter = 0;
            isFlipCounting = true;
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
