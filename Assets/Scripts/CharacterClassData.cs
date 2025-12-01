using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "Game/Class Data")]
public class CharacterClassData : ScriptableObject
{
    public string displayName;
    public Sprite classSprite;
    [TextArea(3, 5)]
    public string classDescription;

    [Header("Class Weapon")]
    public GameObject weaponPrefab;
    public float weaponDamage = 25f;
    public float attackCooldown = 0.5f;

    [Header("Class Stats")]
    public float healthMultiplier = 1f;
    public float speedMultiplier = 1f;
}