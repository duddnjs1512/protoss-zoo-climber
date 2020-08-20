using UnityEngine;

namespace ZooClimber.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Vector2 followOffset;
        [SerializeField] Vector2 threshold;
        [SerializeField] float speed;

        [SerializeField] PlayerCharacter playerCharacter;

        void Start()
        {
            threshold = CalculateThreshold();
        }

        void FixedUpdate()
        {
            if (playerCharacter == null)
            {
                playerCharacter = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerCharacter>();
                return;
            }
            
            var followObjectPos = playerCharacter.transform.position;
            var xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * followObjectPos.x);
            var yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * followObjectPos.y);

            var newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                newPosition.x = followObjectPos.x;
            }

            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPosition.y = followObjectPos.y;
            }

            var playerVelocity = playerCharacter.Rigidbody2D.velocity;
            var moveSpeed = playerVelocity.magnitude > speed ? playerVelocity.magnitude : speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        Vector2 CalculateThreshold()
        {
            var aspect = Camera.main.pixelRect;
            var t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
            t.x -= followOffset.x;
            t.y -= followOffset.y;
            return t;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            var border = CalculateThreshold();
            Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
        }
    }
}
