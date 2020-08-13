using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof (PlayerCharacter))]
    public class PlayerController : CharacterController
    {
        [SerializeField] bool isJumpButtonClicked;
        [SerializeField] bool isTransformClicked;
        [SerializeField] bool isFreeMove;

        PlayerCharacter playerCharacter;
        
        void Start()
        {
            playerCharacter = character as PlayerCharacter;
            Debug.Assert(playerCharacter != null);
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
            var horizontalMove = isFreeMove ? Input.GetAxis("Horizontal") : (float)moveDirection;
            character.Move(horizontalMove, isJumpButtonClicked);
            isJumpButtonClicked = false;

            if (isTransformClicked)
            {
                playerCharacter.RandomTransform();
                isTransformClicked = false;
            }
        }
    }
}
