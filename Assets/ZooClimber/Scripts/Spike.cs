using System;
using UnityEngine;

namespace ZooClimber.Scripts
{
    public class Spike : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other == null || !other.gameObject)
            {
                return;
            }
            
            if (other.gameObject.IsPlayer())
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
