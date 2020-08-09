using UnityEngine;

namespace ZooClimber.Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FormData", order = 1)]
    public class FormData : ScriptableObject
    {
        public string id;
        public float speed;
        public Sprite sprite;
    }
}
