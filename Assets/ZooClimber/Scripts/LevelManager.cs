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
                        var go = new GameObject("Level Manager");
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

        [SerializeField] string mapSceneName;
        [SerializeField] string uiSceneName;
        [SerializeField] string playerPosTag;
        [SerializeField] GameObject playerPrefab;
        
        void Start()
        {
            StartLevel();
        }

        public void StartLevel()
        {
            StartCoroutine(LoadLevelAsync());
        }

        IEnumerator LoadLevelAsync()
        {
            var mapSceneLoad = SceneManager.LoadSceneAsync(mapSceneName, LoadSceneMode.Single);
            while (!mapSceneLoad.isDone)
            {
                yield return null;
            }

            var uiSceneLoad = SceneManager.LoadSceneAsync(uiSceneName, LoadSceneMode.Additive);
            while (!uiSceneLoad.isDone)
            {
                yield return null;
            }

            GameManager.Instance.Reset();

            var playerPos = GameObject.FindGameObjectWithTag(playerPosTag);
            Debug.Assert(playerPos != null);

            var startPos = playerPos.transform.position;
            Destroy(playerPos);

            var player = Instantiate(playerPrefab, startPos, Quaternion.identity);
            player.transform.position = startPos;

            GameManager.Instance.Bind();
            UIManager.Instance.Bind();
        }
    }
}
