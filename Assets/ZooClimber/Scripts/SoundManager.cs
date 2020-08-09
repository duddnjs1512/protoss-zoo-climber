using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZooClimber.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SoundManager>();
                    if (instance == null)
                    {
                        var go = new GameObject("Game Manager");
                        instance = go.AddComponent<SoundManager>();
                    }
                }
                return instance;
            }
        }
        static SoundManager instance;

        [SerializeField] AudioSource[] playerDie;

        void Awake()
        {
            Init();
        }

        void Init()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PlayPlayerDie()
        {
            playerDie[Random.Range(0, playerDie.Length)].Play();
        }
    }
}
