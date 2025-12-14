using UnityEngine;
using System.Collections.Generic;

public class RandomGenRoom : MonoBehaviour
{
    [SerializeField] private Transform _generationTransform;
    [SerializeField] private GameObject _roomTemplate;
    [SerializeField] private Vector2Int _startRoom = new(3, 3);
    [SerializeField] private Vector2Int _generationSize = new(7, 7);
    [SerializeField] private int _numberOfOterations = 3;
    [SerializeField] private float _spacing;
    [SerializeField] private float _spawnChanceChangeCoefficient = 1.5f;
    private int[,] _generation;
    private float _spawnChance = 1f;

    private void Start()
    {
        _generation = new int[ _generationSize.x, _generationSize.y];
        _generation[_startRoom.x, _startRoom.y] = 2;

        for (int i = 0; i < _numberOfOterations; i++)
            _GenerateMassiveIteration();
        _CloseMassiveGeneration();
        _GeneratingRoomsFromMassive();

    }
    private void _GeneratingRoomsFromMassive()
    {
        for (int i = 0; i < _generation.GetLength(0); i++)
        {
            for (int j = 0; j < _generation.GetLength(1); j++)
            {
                if (_generation[i, j] == 1)
                {
                    GameObject instRoom = Instantiate(_roomTemplate, new Vector2((i - _startRoom.x) * _spacing, (j - _startRoom.y) * _spacing), Quaternion.identity, _generationTransform);
                    Rooms rooms = instRoom.GetComponent<Rooms>();
                    rooms.SetDoors(CheckRoomsNearly(new Vector2Int(i, j)));
                    rooms.type = RoomType.Peaceful;
                }
            }
        }
    }
 
      private DoorSet CheckRoomsNearly(Vector2Int room)
      {
          bool IsSpacePositiveX = room.x + 1 < _generation.GetLength(0) - 1 && room.y + 1 >= 0 && _generation[room.x + 1, room.y] == 0;
          bool IsSpaceNegativeX = room.x - 1 < _generation.GetLength(0) - 1 && room.y - 1 >= 0 && _generation[room.x - 1, room.y] == 0;
          bool IsSpacePositiveY = room.y + 1 < _generation.GetLength(1) - 1 && room.y + 1 >= 0 && _generation[room.x, room.y + 1] == 0;
          bool IsSpaceNegativeY = room.y - 1 < _generation.GetLength(1) - 1 && room.y - 1 >= 0 && _generation[room.x, room.y - 1] == 0;

          return new DoorSet(IsSpaceNegativeY, IsSpacePositiveY,  IsSpaceNegativeX, IsSpacePositiveX);
      } 

    private void _GenerateMassiveIteration()
    {
        List<Vector2Int> hotRooms = new();
        for (int i = 0; i < _generation.GetLength(0); i++)
        {
            for (int j = 0; j < _generation.GetLength(1); j++)
            {
                if (_generation[i, j] == 2)
                {
                    hotRooms.Add(new Vector2Int(i, j));
                }
            }
        }
        foreach (Vector2Int room in hotRooms)
        {
            _TryToSpawnRoomInMassive(room, new Vector2Int(1, 0));
            _TryToSpawnRoomInMassive(room, new Vector2Int(-1, 0));
            _TryToSpawnRoomInMassive(room, new Vector2Int(0, 1));
            _TryToSpawnRoomInMassive(room, new Vector2Int(0, -1));

            _generation[room.x, room.y] = 1;
        }
    } 
    private void _TryToSpawnRoomInMassive(Vector2Int room, Vector2Int offSet)
    {
        bool CanSpawnAxisX = room.x + offSet.x <= _generation.GetLength(0) - 1 && room.x + offSet.x >= 0 && _generation[room.x + offSet.x, room.y] == 0;
        //bool CanSpawnAxisy = room.y + offSet.y <= _generation.GetLength(1) - 1 && room.y + offSet.y >= 0 && _generation[room.x + room.y, offSet.y] == 0;

        bool CanSpawnAxisy = room.y + offSet.y < _generation.GetLength(1) &&  room.y + offSet.y >= 0 &&  _generation[room.x, room.y + offSet.y] == 0;

        if (CanSpawnAxisX)
        {
            float chance = Random.Range(0f, 1f);

            if (chance <= _spawnChance)
            {
                _generation[room.x + offSet.x, room.y] = 2;
                _spawnChance /= _spawnChanceChangeCoefficient;
            }
        }

        if (CanSpawnAxisy)
        {
            float chance = Random.Range(0f, 2f);

            if (chance <= _spawnChance)
            {
                _generation[room.x + offSet.x, room.y] = 2;
                _spawnChance /= _spawnChanceChangeCoefficient;
            }
        }

    } 
    private void _CloseMassiveGeneration()
    {
        for (int i = 0; i < _generation.GetLength(0); i++)
        {
            for (int j = 0; j < _generation.GetLength(1); j++)
            {
                if (_generation[i, j] == 2)
                {
                    _generation[i, j] = 1;
                }
            }
        }
    }
}

