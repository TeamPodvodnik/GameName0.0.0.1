using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    protected GameObject _player;
    protected CharacterClassData _currentClass;

    void Start()
    {
        _player = gameObject;
        _currentClass = PlayerClassManager.Instance?.GetSelectedClass();
        SetupClassAbilities();
    }

    protected virtual void SetupClassAbilities()
    {

    }

    void Update()
    {
        if (_currentClass == null) return;
        HandleClassPowers();
    }

    protected virtual void HandleClassPowers()
    {

    }
}