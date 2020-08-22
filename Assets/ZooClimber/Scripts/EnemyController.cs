using UnityEngine;

namespace ZooClimber.Scripts
{
    public class EnemyController : CharacterController
    {
        public enum EnemyForm
        {
            None = -1,
            Snake = 0,
            Lion = 1,
            Eagle = 2,
            Cat = 3
        }

        public enum JumpDirection
        {
            Up = 0,
            Down = 1
        }

        [SerializeField] EnemyForm enemyForm;

        protected override void OnFlip()
        {
            if (enemyForm == EnemyForm.Cat)
            {
                CatOnFlip();
            }
        }

        void CatOnFlip()
        {
            var jumpOffset = Vector3.zero;
            var randomJumpDir = (JumpDirection)Random.Range(0, 2);
            if (randomJumpDir == JumpDirection.Up)
            {
                jumpOffset = new Vector3(0, 5, 0);
            }
            else if (randomJumpDir == JumpDirection.Down)
            {
                jumpOffset = new Vector3(0, -5, 0);
            }

            movable.Rigidbody2D.velocity = Vector2.zero;
            
            var o = transform.position;
            transform.position = o + jumpOffset;
            
            Debug.Log($"Cat on flip: {randomJumpDir.ToString()}");
        }
    }
}
