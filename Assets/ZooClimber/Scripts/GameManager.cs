using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZooClimber.Scripts
{
    public static class ExtensionMethods
    {
        public static bool IsPlayer(this GameObject go)
        {
            return go.CompareTag("Player");
        }
    }
    
    public class GameManager : MonoBehaviour
    {
        public const int MIN_PLAYER_HP = 0;
        public const int MAX_PLAYER_HP = 5;
        public const int MIN_FLOOR = 1;
        public const float WORLD_MIN_POSITION_X = -20f;
        public const float WORLD_MAX_POSITION_X = 20f;
        public const string SPIKE_LAYER_NAME = "Spike";
        
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        var go = new GameObject("Game Manager");
                        instance = go.AddComponent<GameManager>();
                    }
                }
                return instance;
            }
        }
        static GameManager instance;

        public PlayerCharacter PlayerCharacter
        {
            get => playerCharacter;
        }
        [SerializeField] PlayerCharacter playerCharacter;

        // Masks
        public LayerMask GroundMask => groundMask;
        [SerializeField] LayerMask groundMask;
        
        public LayerMask WallMask => wallMask;
        [SerializeField] LayerMask wallMask;

        public int PlayerHp
        {
            get => playerHp;
            set
            {
                playerHp = value;
                if (playerHp < 0)
                {
                    playerHp = 0;
                }
                
                UIManager.Instance.SetHeart(value);

                if (playerHp <= 0)
                {
                    GameOver();
                }
            }
        }
        int playerHp = 5;

        public int CurrentFloor
        {
            get => currentFloor;
            set
            {
                currentFloor = value;
                if (currentFloor < 1)
                {
                    currentFloor = MIN_FLOOR;
                }

                UIManager.Instance.SetFloor(value);
            }
        }
        private int currentFloor;

        public void Bind()
        {
            if (playerCharacter == null)
            {
                playerCharacter = FindObjectOfType<PlayerCharacter>();
                Debug.Assert(playerCharacter != null);
            }
        }

        public void Reset()
        {
            if (PlayerCharacter != null)
            {
                Destroy(playerCharacter.gameObject);
            }
            
            playerHp = MAX_PLAYER_HP;
        }

        void Awake()
        {
            Init();
        }

        void Init()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            
            SoundManager.Instance.PlayPlayerDie();
            
            LevelManager.Instance.StartLevel();
            
            Init();
        }
    }
}
