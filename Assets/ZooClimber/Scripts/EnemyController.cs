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

        [SerializeField] EnemyForm enemyForm;

        protected override void OnFlip()
        {
        }
    }
}
