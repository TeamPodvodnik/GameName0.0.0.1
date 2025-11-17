using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ToggleGroup difficultyToggleGroup;
    [SerializeField] private string gameSceneName = "level1";

    public void PlayGame()
    {
        Debug.Log("PlayGame called");
        SaveSelectedDifficulty();
        SceneManager.LoadScene(gameSceneName);
    }
    
    private void SaveSelectedDifficulty()
    {
        Debug.Log("SaveSelectedDifficulty called");

        if (difficultyToggleGroup == null)
        {
            Debug.LogError("ToggleGroup is NULL!");
            return;
        }

        bool foundToggle = false;

        foreach (Toggle toggle in difficultyToggleGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                DifficultyToggle difficultyToggle = toggle.GetComponent<DifficultyToggle>();
                if (difficultyToggle != null)
                {
                    Debug.Log($"Found toggle: {toggle.name}, difficulty index: {difficultyToggle.difficultyIndex}");
                    DifficultyManager.Instance.SetDifficulty(difficultyToggle.difficultyIndex);
                    foundToggle = true;
                    break;
                }
            }
        }

        if (!foundToggle)
        {
            Debug.LogWarning("No active toggle found!");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
}