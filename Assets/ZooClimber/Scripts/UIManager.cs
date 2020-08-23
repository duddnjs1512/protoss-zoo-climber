using System;
using UnityEngine;
using TMPro;

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
        [SerializeField] GameObject[] hearts;
        [SerializeField] TextMeshProUGUI floorIndicator;
        [SerializeField] TextMeshProUGUI scoreIndicator;

        public void Bind()
        {
            canvas.worldCamera = FindObjectOfType<Camera>();
        }

        public void SetHeart(int value)
        {
            if (value > hearts.Length)
            {
                value = hearts.Length;
            }

            for (var i = 0; i < hearts.Length; i++)
            {
                hearts[i].SetActive(i < value);
            }
        }

        public void SetFloor(int value)
        {
            floorIndicator.SetText($"{value.ToString()}F");
        }
    }
}
