using UnityEngine;

namespace ZooClimber.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                    if (instance == null)
                    {
                        var go = new GameObject("UI Manager");
                        instance = go.AddComponent<UIManager>();
                    }
                }
                return instance;
            }
        }
        static UIManager instance;

        [SerializeField] Canvas canvas;

        void Start()
        {
            canvas.worldCamera = FindObjectOfType<Camera>();
        }
    }
}
