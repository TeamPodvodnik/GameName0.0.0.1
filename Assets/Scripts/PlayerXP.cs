using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerXP : MonoBehaviour
{
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;
    public int currentLevel = 1;
    public int maxLevel = 50;

    [SerializeField] private Image _xpBarImage;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _xpBarText;
    [SerializeField] private Lvlhandler _lvlHandler;

    private void Start()
    {
        if (_lvlHandler == null)
            _lvlHandler = FindFirstObjectByType<Lvlhandler>();

        UpdateXPUI();
    }

    public void AddXP(float baseXP)
    {
        if (currentLevel >= maxLevel) return;

        float multiplier = DifficultyManager.Instance.GetCurrentDifficulty().xpMultiplier;
        float gainedXP = baseXP * multiplier;
        currentXP += gainedXP;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateXPUI();
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel;
        xpToNextLevel *= 1.5f;

        if (currentLevel >= maxLevel)
        {
            currentLevel = maxLevel;
            currentXP = xpToNextLevel;
        }

        if (_lvlHandler != null)
        {
            _lvlHandler.CheckLevel(currentLevel);
        }

    }

    private void UpdateXPUI()
    {
        if (_xpBarImage != null)
        {
            _xpBarImage.fillAmount = currentXP / xpToNextLevel;
        }

        if (_levelText != null)
        {
            _levelText.text = $"{currentLevel}";
        }

        if (_xpBarText != null)
        {
            _xpBarText.text = $"{currentXP:F0} / {xpToNextLevel:F0} XP";
        }
    }
}