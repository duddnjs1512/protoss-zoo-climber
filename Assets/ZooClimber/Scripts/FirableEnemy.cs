using UnityEngine;

namespace ZooClimber.Scripts
{
    public class FirableEnemy : MonoBehaviour
    {
        [SerializeField] float fireRate;
        [SerializeField] Projectile projectile;

        float fireCounter;
        CharacterController characterController;

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            fireCounter += Time.deltaTime;

            if (fireCounter >= fireRate)
            {
                var bullet = Instantiate(projectile, transform.position, Quaternion.identity);
                bullet.Init(characterController.FaceDirectionToVector);
                fireCounter = 0f;
            }
        }
    }
}
