using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] doors;
    public MobSpawner[] spawners;
    private int enemiesLeft;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!activated && col.CompareTag("Player"))
        {
            activated = true;
            CloseDoors();

            enemiesLeft = spawners.Length;
            foreach (var sp in spawners)
                sp.Spawn(this);
        }
    }

    public void EnemyDied()
    {
        enemiesLeft--;

        if (enemiesLeft <= 0)
            OpenDoors();
    }

    void CloseDoors()
    {
        foreach (var d in doors)
            d.SetActive(true);
    }

    void OpenDoors()
    {
        foreach (var d in doors)
            d.SetActive(false);
    }
}
