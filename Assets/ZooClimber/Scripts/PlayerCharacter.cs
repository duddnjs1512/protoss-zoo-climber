using System;
using UnityEngine;
using ZooClimber.Data;
using Random = UnityEngine.Random;

namespace ZooClimber.Scripts
{
    public class PlayerCharacter : MovableCharacter
    {
        public enum PlayerForm
        {
            Rat,
            Turtle,
            Sparrow
        }

        readonly int TOTAL_PLAYER_FORM_LENGTH = Enum.GetNames(typeof(PlayerForm)).Length;
        
        public PlayerForm ActivePlayerForm
        {
            get => activePlayerForm;
            set
            {
                activeFormIndex = (int) value;
                spriteRenderer.sprite = playerFormData[activeFormIndex].sprite;
                formData = playerFormData[activeFormIndex];
                activePlayerForm = value;
            }
        }
        PlayerForm activePlayerForm;
        int activeFormIndex;
        
        [SerializeField] FormData[] playerFormData;
        [SerializeField] SpriteRenderer spriteRenderer;
        
        public void RandomTransform()
        {
            var oldForm = activePlayerForm;
            var newFormIndex = Random.Range(0, 3);
            var newForm = (PlayerForm)newFormIndex;
            if (oldForm == newForm)
            {
                newForm = (PlayerForm)((newFormIndex + 1) % TOTAL_PLAYER_FORM_LENGTH);
            }
            ActivePlayerForm = newForm;
            Debug.Log($"Transform from \"{oldForm}\" to \"{newForm}\"");
        }
    }
}
