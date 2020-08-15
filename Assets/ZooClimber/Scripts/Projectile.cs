using System;
using UnityEngine;

namespace ZooClimber.Scripts
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D => rigidbody2d;
        Rigidbody2D rigidbody2d;

        [SerializeField] float speed;
        [SerializeField] Vector2 direction;

        [SerializeField] float hitForce = 5;

        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        public void Init(Vector2 direction)
        {
            this.direction = direction;
        }

        void FixedUpdate()
        {
            rigidbody2d.velocity = direction * speed;

            if (transform.position.x < GameManager.WORLD_MIN_POSITION_X || transform.position.x > GameManager.WORLD_MAX_POSITION_X)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
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
        }
    }
}
