﻿using UnityEngine;

namespace ZooClimber.Scripts
{
    [RequireComponent(typeof (PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        public float minXPos = -10f;
        public float maxXPos = 10f;
        
        enum MoveDirection
        {
            Left = -1,
            Right = 1
        }
        
        [SerializeField] PlayerCharacter playerCharacter;

        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] MoveDirection moveDirection = MoveDirection.Right; 
        [SerializeField] bool isJumped;
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

            if (transform.position.x < minXPos + playerCharacter.Collider2D.bounds.extents.x ||
                playerCharacter.IsBlocked && moveDirection == MoveDirection.Left)
            {
                moveDirection = MoveDirection.Right;
                spriteRenderer.flipX = false;
            }
            else if (transform.position.x > maxXPos - playerCharacter.Collider2D.bounds.extents.x ||
                     playerCharacter.IsBlocked && moveDirection == MoveDirection.Right)
            {
                moveDirection = MoveDirection.Left;
                spriteRenderer.flipX = true;
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
