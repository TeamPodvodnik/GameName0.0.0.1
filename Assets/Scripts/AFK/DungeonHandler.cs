using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Tilemaps;
using static DungeonHandler;

public class DungeonHandler : MonoBehaviour
{
    [Header("Main Settings")]
    public DungeaonSettings[] dungeonSettings;


    [System.Serializable]
    public struct DungeaonSettings
    {
        public string name;
        public Tilemap mapDungeaon;
        public DoorsPlace[] doorsPlaces;
        public AreaSpawnResources[] areaSpawnResources;
        public TargetSpawnResources[] targetSpawnResources;
        public EnemyFolder[] enemyFolders;
        public BossFolder[] bossFolders;
        public StatueSpawn[] statueSpawns;
        public CurrentDifficulty currentDifficulty;
    }
    [System.Serializable]
    public struct DoorsPlace
    {
        public string name;
        public NeededDifficulty neededDifficultyArea;
        public GameObject targetDoor;
        public GameObject doorClosedPrefab;
    }
    [System.Serializable]
    public struct AreaSpawnResources
    {
        public string name;
        public NeededDifficulty neededDifficultyArea;
        public GameObject area;
        public int minObject;
        public int maxObject;
        public float minDistanceBetweenObjects;
        public AreaResources[] areaResources;
    }
    [System.Serializable]
    public struct AreaResources
    {
        public string name;
        public GameObject prefabObject;
        [Range(0, 100)]
        public float chance;
    }
    [System.Serializable]
    public struct TargetSpawnResources
    {
        public string name;
        public NeededDifficulty neededDifficultyTarget;
        public TargetResource[] targetResources;
    }
    [System.Serializable]
    public struct TargetResource
    {
        public GameObject target;
        public GameObject resoource;
    }
    [System.Serializable]
    public struct EnemyFolder
    {
        public string name;
        public NeededDifficulty neededDifficultyEnemy;
        public GameObject area;
        public GameObject enemyPrefab;
        public float cooldownSpawnEnemy;
    }
    [System.Serializable]
    public struct BossFolder
    {
        public string name;
        public NeededDifficulty neededDifficultyBoss;
        public GameObject area;
        public GameObject bossPrefab;
    }

    [System.Serializable]
    public struct StatueSpawn
    {
        public string name;
        public NeededDifficulty neededDifficultyStatue;
        public NewDifficulty newDifficulty;
        public GameObject areaSpawn;
        public GameObject statuePrefab;
        public float destroyStatueTimer;
    }

    public enum CurrentDifficulty { Default, Medium, Hard, Awaked }
    public enum NeededDifficulty { Default, Medium, Hard, Awaked }
    public enum NewDifficulty { Default, Medium, Hard, Awaked }

}
