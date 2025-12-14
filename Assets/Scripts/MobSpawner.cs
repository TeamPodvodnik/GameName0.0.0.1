using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public void Spawn(Room room)
    {
        GameObject e = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        Enemy en = e.GetComponent<Enemy>();
        en.room = room;
    }
}
