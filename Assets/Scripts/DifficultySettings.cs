using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "Game/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    [System.Serializable]
    public struct DifficultyLevel
    {
        public string name;
        public float enemyHealth;
        public float enemyDamage;
        public float xpMultiplier;
    }

    public DifficultyLevel[] difficulties = new DifficultyLevel[]
    {
        new DifficultyLevel { name = "Easy", enemyHealth = 50f, enemyDamage = 10f, xpMultiplier = 0.8f},
        new DifficultyLevel { name = "Normal", enemyHealth = 100f, enemyDamage = 20f, xpMultiplier = 1f},
        new DifficultyLevel { name = "Hard", enemyHealth = 200f, enemyDamage = 30f, xpMultiplier = 1.5f}
    };
}