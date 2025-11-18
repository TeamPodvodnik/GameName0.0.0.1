using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager _current;

    void Start()
    {
        if (_current != null)
        {
            Destroy(gameObject);
            return;
        }
        _current = this;
    }

    public void AllIgnorePlayer(bool ignore)
    {
        var all = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        foreach (var e in all)
        {
            e.SetPlayerIgnored(ignore);
        }
    }

    public void FlipIgnore()
    {
        var all = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        if (all.Length > 0)
        {
            AllIgnorePlayer(!all[0].IsPlayerIgnored());
        }
    }
}