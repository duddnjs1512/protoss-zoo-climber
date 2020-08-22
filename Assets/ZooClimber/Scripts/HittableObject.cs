using UnityEngine;

namespace ZooClimber.Scripts
{
    public class HittableObject : MonoBehaviour
    {
        [SerializeField] float hitForce = 5;

        void OnTriggerEnter2D(Collider2D other)
        {
            TryHitPlayer(other);
        }

        void OnTriggerStay2D(Collider2D other)
        {
            TryHitPlayer(other);
        }

        void TryHitPlayer(Collider2D other)
        {
            if (other == null || !other.gameObject)
            {
                return;
            }

            var playerCharacter = other.gameObject.GetComponent<PlayerController>();
            if (playerCharacter != null)
            {
                OnPlayerHit(playerCharacter);
            }
        }

        void OnPlayerHit(PlayerController playerController)
        {
            Debug.Log($"\"{playerController.gameObject.name}\" collided with \"{gameObject.name}\"");
            playerController.Hit(transform.position, hitForce);
            
            GameManager.Instance.PlayerHp--;
        }
    }
}
