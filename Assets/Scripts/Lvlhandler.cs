using UnityEngine;

public class Lvlhandler : MonoBehaviour
{
    private PlayerHealth _playerEntity;
    private PlayerClassManager _classManager;

    public LevelData[] levels;

    [System.Serializable]
    public struct LevelData
    {
        public int level;
        public bool hasSkill;
        public SkillData[] skillSetting;
    }

    [System.Serializable]
    public struct SkillData
    {
        public string name;
        public SkillType skillType;
        public CharacterClassData forClass;
        public MonoBehaviour skillScript;
    }

    public enum SkillType { Active, Passive }

    private void Start()
    {
        _playerEntity = FindFirstObjectByType<PlayerHealth>();
        _classManager = PlayerClassManager.Instance;
        DisableAllSkills();
    }

    private void DisableAllSkills()
    {
        foreach (LevelData levelData in levels)
        {
            foreach (SkillData skill in levelData.skillSetting)
            {
                if (skill.skillScript != null)
                {
                    skill.skillScript.enabled = false;
                }
            }
        }
    }

    public void CheckLevel(int currentLevel)
    {
        CharacterClassData currentClass = _classManager.GetSelectedClass();
        if (currentClass == null) return;

        foreach (LevelData levelData in levels)
        {
            if (levelData.hasSkill && currentLevel == levelData.level)
            {
                foreach (SkillData skill in levelData.skillSetting)
                {
                    if (skill.forClass == currentClass && skill.skillScript != null)
                    {
                        skill.skillScript.enabled = true;
                        if (skill.forClass.displayName.Contains("Жрец"))
                        {
                            PriestAbilities priest = skill.skillScript as PriestAbilities;
                            if (priest != null) priest.UnlockAbility();
                        }
                        else if (skill.forClass.displayName.Contains("Волшебник"))
                        {
                            MageAbilities mage = skill.skillScript as MageAbilities;
                            if (mage != null) mage.UnlockAbility();
                        }
                        else if (skill.forClass.displayName.Contains("Рыцарь"))
                        {
                            KnightAbilities knight = skill.skillScript as KnightAbilities;
                            if (knight != null) knight.UnlockAbility();
                        }
                    }
                }
            }
        }
    }
}