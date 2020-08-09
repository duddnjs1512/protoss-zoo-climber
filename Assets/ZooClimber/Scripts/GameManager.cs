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

        void Init()
        {
            if (playerCharacter == null)
            {
                playerCharacter = FindObjectOfType<PlayerCharacter>();
                Debug.Assert(playerCharacter != null);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            
            SoundManager.Instance.PlayPlayerDie();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            Init();
        }
    }
}
