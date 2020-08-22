using UnityEngine;

namespace ZooClimber.Scripts
{
    public class HittableEnemy : MonoBehaviour
    {
        [SerializeField] PlayerCharacter.PlayerForm counterForm;
        [SerializeField] int damage = 1;
        [SerializeField] float hitForce = 5;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            TryHitPlayer(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            TryHitPlayer(other);
        }

        void TryHitPlayer(Collision2D other)
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
            playerController.Hit(transform.position, hitForce, damage);

            if (counterForm == PlayerCharacter.PlayerForm.All || 
                counterForm == playerController.PlayerCharacter.ActivePlayerForm)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
