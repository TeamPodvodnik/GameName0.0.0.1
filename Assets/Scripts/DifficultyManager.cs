using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    public int selectedDifficultyIndex { get; private set; } = 1;

    private DifficultySettings difficultySettings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadDifficultySettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDifficultySettings()
    {
        difficultySettings = Resources.Load<DifficultySettings>("DifficultySettings");
        if (difficultySettings == null)
        {
            Debug.LogError("DifficultySettings not found in Resources!");
        }
    }

    public void SetDifficulty(int difficultyIndex)
    {
        if (difficultySettings != null && difficultyIndex >= 0 && difficultyIndex < difficultySettings.difficulties.Length)
        {
            selectedDifficultyIndex = difficultyIndex;
            Debug.Log($"Difficulty set to index: {difficultyIndex} ({difficultySettings.difficulties[difficultyIndex].name})");
        }
    }

    public DifficultySettings.DifficultyLevel GetCurrentDifficulty()
    {
        if (difficultySettings != null && selectedDifficultyIndex < difficultySettings.difficulties.Length)
        {
            return difficultySettings.difficulties[selectedDifficultyIndex];
        }

        return new DifficultySettings.DifficultyLevel { name = "Normal", enemyHealth = 100f, enemyDamage = 20f };
    }
}