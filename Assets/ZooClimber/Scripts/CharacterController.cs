﻿using UnityEngine;

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
            if (CanFlip() && moveDirection == MoveDirection.Left && character.IsGrounded)
            {
                moveDirection = MoveDirection.Right;
                formSprite.flipX = false;
            }
            else if (CanFlip() && moveDirection == MoveDirection.Right && character.IsGrounded)
            {
                moveDirection = MoveDirection.Left;
                formSprite.flipX = true;
            }
        }

        bool CanFlip()
        {
            return character.IsBlocked;
        }
        
        protected virtual void UpdateCharacter()
        {
            character.Move((float)moveDirection, false, false, Vector3.zero, 0f);
        }
    }
}
