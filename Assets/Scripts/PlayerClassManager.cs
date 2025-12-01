using System;
using UnityEngine;

public class PlayerClassManager : MonoBehaviour
{
    private static PlayerClassManager _instance;
    public static PlayerClassManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectByType<PlayerClassManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("PlayerClassManager");
                    _instance = obj.AddComponent<PlayerClassManager>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    private static T FindObjectByType<T>()
    {
        throw new NotImplementedException();
    }

    private CharacterClassData _selectedClass;
    private bool _hasSelectedClass = false;

    public void SetSelectedClass(CharacterClassData classData)
    {
        _selectedClass = classData;
        _hasSelectedClass = true;
    }

    public CharacterClassData GetSelectedClass()
    {
        return _selectedClass;
    }

    public bool HasClassSelected()
    {
        return _hasSelectedClass;
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}