using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZooClimber.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LevelManager>();
                    if (instance == null)
                    {
                        var go = new GameObject("Game Manager");
                        instance = go.AddComponent<LevelManager>();
                    }
                }
                return instance;
            }
        }
        static LevelManager instance;

        void Awake()
        {
            Init();
        }

        void Init()
        {
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] string defaultSceneToLoad;
        [SerializeField] string playerPosTag;
        [SerializeField] GameObject playerPrefab;
        
        void Start()
        {
            StartCoroutine(LoadLevelAsync());
        }

        IEnumerator LoadLevelAsync()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(defaultSceneToLoad);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var playerPos = GameObject.FindGameObjectWithTag(playerPosTag);
            Debug.Assert(playerPos != null);

            var startPos = playerPos.transform.position;
            Destroy(playerPos);

            var player = Instantiate(playerPrefab, startPos, Quaternion.identity);
            player.transform.position = startPos;

            GameManager.Instance.Bind();
        }
    }
}
