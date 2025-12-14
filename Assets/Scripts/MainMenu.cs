using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ToggleGroup difficultyToggleGroup;
    [SerializeField] private string gameSceneName = "level1";
    [SerializeField] private Button playButton;

    private void Update()
    {
        if (playButton != null)
        {
            playButton.interactable = PlayerClassManager.Instance.HasClassSelected();
        }
    }

    public void PlayGame()
    {
        if (!PlayerClassManager.Instance.HasClassSelected())
        {
            return;
        }

        SaveSelectedDifficulty();
        SceneManager.LoadScene(gameSceneName);
    }

    private void SaveSelectedDifficulty()
    {

        if (difficultyToggleGroup == null)
        {
            return;
        }

        foreach (Toggle toggle in difficultyToggleGroup.ActiveToggles())
        {
            if (toggle.isOn)
            {
                DifficultyToggle difficultyToggle = toggle.GetComponent<DifficultyToggle>();
                if (difficultyToggle != null)
                {
                    DifficultyManager.Instance.SetDifficulty(difficultyToggle.difficultyIndex);
                    break;
                }
            }
        }
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
}