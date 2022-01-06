using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private List<GameObject> currentRooms;

    [SerializeField] private GameObject[] availableObjects;
    [SerializeField] private List<GameObject> objects;

    [SerializeField] private float MinObjectDistance = 5.0f;
    [SerializeField] private float MaxObjectDistance = 10.0f;

    [SerializeField] private float MinObjectY = -1.4f;
    [SerializeField] private float MaxObjectY = 1.4f;

    [SerializeField] private float objectMinRotation = -45.0f;
    [SerializeField] private float objectMaxRotation = 45.0f;

    private float screenWidth;
    void Start()
    {
        screenWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        GenerateRoomIfItRequired();
        GenerateObjectsIfItRequired();
    }
    private void AddRoom(float farthestRoomEndX)
    {
        int index = Random.Range(0, rooms.Length);
        GameObject room = Instantiate(rooms[index]) as GameObject;
        float roomWidth = room.transform.Find("FLOOR").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth*0.5f;
        room.transform.position = new Vector2(roomCenter, 0);
        currentRooms.Add(room);
    }
    private void GenerateRoomIfItRequired()
    {
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        float RemoveRoomX = playerX - screenWidth;
        float AddRoomX = playerX + screenWidth;      
        float farthestRoomEndX = 0;      
        foreach(var room in currentRooms)
        {
            float roomWidth = room.transform.Find("FLOOR").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth*0.5f);
            float roomEndX = roomStartX + roomWidth;
            if (roomStartX > AddRoomX)
                addRooms = false;
            if (roomEndX < RemoveRoomX)
                roomsToRemove.Add(room);
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        foreach(var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        if (addRooms) AddRoom(farthestRoomEndX);
    }
    private void GenerateObjectsIfItRequired()
    {
        List<GameObject> objsToRemove = new List<GameObject>();
        float playerX = transform.position.x;
        float RemoveObjectX = playerX - screenWidth;
        float AddObjectX = playerX + screenWidth;
        float farthestObjectX = 0;
        foreach (var obj in objects)
        {
            float objX = obj.transform.position.x;
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            if (objX < RemoveObjectX)
                objsToRemove.Add(obj);
        }
        foreach (var obj in objsToRemove)
        {
            objects.Remove(obj);
            Destroy(obj);
        }
        if (farthestObjectX < AddObjectX) AddObject(farthestObjectX);
    }
    private void AddObject(float lastObjectX)
    {
        int randomIndex = Random.Range(0, availableObjects.Length);
        GameObject obj = Instantiate(availableObjects[randomIndex]) as GameObject;
        float ObjectPositionX = lastObjectX + Random.Range(MinObjectDistance, MaxObjectDistance);
        float ObjectPositionY = Random.Range(MinObjectY, MaxObjectY);
        obj.transform.position = new Vector2(ObjectPositionX, ObjectPositionY);
        float rotation = Random.Range(objectMinRotation, objectMaxRotation);
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
        objects.Add(obj);
    }
}
