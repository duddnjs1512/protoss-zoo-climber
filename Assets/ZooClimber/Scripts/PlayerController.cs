using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof (PlayerCharacter))]
    public class PlayerController : CharacterController
    {
        [SerializeField] bool isJumpButtonClicked;
        [SerializeField] bool isTransformClicked;
        [SerializeField] bool isFreeMove;
        [SerializeField] bool isHit;

        float hitForce;
        Vector3 hitSourcePos;

        public PlayerCharacter PlayerCharacter => playerCharacter;
        PlayerCharacter playerCharacter;
        
        void Start()
        {
            playerCharacter = movable as PlayerCharacter;
            Debug.Assert(playerCharacter != null);
        }

        public void Hit(Vector3 hitSourcePos, float hitForce, int damage)
        {
            isHit = true;
            this.hitForce = hitForce;
            this.hitSourcePos = hitSourcePos;

            if (!playerCharacter.IsInvincible)
            {
                GameManager.Instance.PlayerHp -= damage;
            }
        }

        protected override void UpdateController()
        {
            if (!isJumpButtonClicked)
            {
                isJumpButtonClicked = Input.GetButtonDown("Jump");
            }
            
            if (!isTransformClicked)
            {
                isTransformClicked = Input.GetButtonDown("Transform");
            }

            base.UpdateController();
        }

        protected override void UpdateCharacter()
        {
            var horizontalMove = isFreeMove ? Input.GetAxis("Horizontal") : (float)faceDirection;
            movable.Move(horizontalMove, isJumpButtonClicked, isHit, hitSourcePos, hitForce);
            isJumpButtonClicked = false;
            isHit = false;

            if (isTransformClicked)
            {
                playerCharacter.RandomTransform();
                isTransformClicked = false;
            }

            GameManager.Instance.CurrentFloor = Mathf.FloorToInt(playerCharacter.transform.position.y * 0.2f) + 1;
        }
    }
}
