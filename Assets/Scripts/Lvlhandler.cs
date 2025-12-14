using UnityEngine;

public class Lvlhandler : MonoBehaviour
{
    private PlayerHealth _playerEntyti;

    public level[] levels;

    [System.Serializable]
    public struct level
    {
        public int lvl;
        public int currentxp;
        public bool hasSkill;
        public skill[] skillSetting;
    }

    [System.Serializable]
    public struct skill
    {
        public string name;
        public SkillType TypeOfSkill;
        public GameObject selfSkill;
    }

    public enum SkillType { acitve, passive }


    private void Start()
    {
        _playerEntyti = FindFirstObjectByType<PlayerHealth>();
        _playerEntyti.GetComponent<PriestAbilities>().enabled = false;
        _playerEntyti.GetComponent<KnightAbilities>().enabled = false;
        _playerEntyti.GetComponent<MageAbilities>().enabled = false;
    }

    public void CheckLevel(int currentLevel)
    {
        foreach (var levelData in levels)
        {
            if (levelData.hasSkill != false)
            {
                if (currentLevel >= levelData.lvl)
                {
                    _playerEntyti.GetComponent<PriestAbilities>().enabled = true;
                }
            }
        }
    }
}
