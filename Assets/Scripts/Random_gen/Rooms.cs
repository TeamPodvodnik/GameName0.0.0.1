using System;
using UnityEngine;

public enum RoomType
{
    Hostile,
    Peaceful
}

public class Rooms : MonoBehaviour
{
    [SerializeField] private GameObject _upDoor;
    [SerializeField] private GameObject _downDoor;
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;

    [SerializeField] public RoomType type;
    [SerializeField] private DoorSet _startDoorSet;
    public void SetDoors(DoorSet doorSet)
    {
        _startDoorSet = doorSet;

        if (_upDoor != null) _upDoor.SetActive(_startDoorSet._upOpen);
        if (_downDoor != null) _downDoor.SetActive(_startDoorSet._downOpen);
        if (_leftDoor != null) _leftDoor.SetActive(_startDoorSet._leftOpen);
        if (_rightDoor != null) _rightDoor.SetActive(_startDoorSet._rightOpen);
    }

    private void CloseDoors()
    {
      if (_upDoor != null) _upDoor.SetActive(true);
      if (_downDoor != null) _downDoor.SetActive(true);
      if (_leftDoor != null) _leftDoor.SetActive(true);
      if (_rightDoor != null) _rightDoor.SetActive(true);
    }
    
    public void OpenDoors()
    {
        SetDoors(_startDoorSet);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.GetComponent<Hero>() && type == RoomType.Hostile)
        {

        }
    }

}
[Serializable]
public class DoorSet
{
    public bool _upOpen;
    public bool _downOpen;
    public bool _leftOpen;
    public bool _rightOpen;

    public DoorSet(bool up, bool down, bool right, bool left)
    {
        _upOpen = up;
        _downOpen = down;
        _leftOpen = left;
        _rightOpen = right;
    }
    public DoorSet() { }
}
