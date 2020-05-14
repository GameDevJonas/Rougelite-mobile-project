using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetAssigner : MonoBehaviour
{
    [SerializeField]
    Texture2D[] sheetsNormal;

    public Texture2D startSheet;
    public Texture2D lastSheet;

    [SerializeField]
    GameObject RoomObj;
    public Vector2 roomDimensions = new Vector2(16 * 17, 16 * 9);
    public Vector2 gutterSize = new Vector2(16 * 9, 16 * 4);

    public bool startRoomGenerated = false;
    public bool lastRoomGenerated = false;

    public LevelGeneration theGen;

    public List<GameObject> roomsCreated = new List<GameObject>();

    private void Awake()
    {
        theGen = GetComponent<LevelGeneration>();
    }

    public void Assign(Room[,] rooms)
    {
        int lenghtOfRooms = rooms.Length;
        int i = 0;
        //Debug.Log("Max rooms is " + theGen.numberOfRooms);
        //Debug.Log(theGen.roomList.Count);
        foreach (Room room in rooms)
        {
            i++;
            //skip point where there is no room
            if (room == null)
            {
                continue;
            }

            if (!startRoomGenerated)
            {
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(startSheet, room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
                startRoomGenerated = true;
                roomsCreated.Add(myRoom.gameObject);
                //Debug.Log(roomsCreated.Count, roomsCreated[roomsCreated.Count - 1]);
            }
            else if(roomsCreated.Count < theGen.numberOfRooms - 1)
            {
                //pick a random index for the array
                int index = Mathf.RoundToInt(Random.value * (sheetsNormal.Length - 1));
                //find position to place room
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(sheetsNormal[index], room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
                roomsCreated.Add(myRoom.gameObject);
                //Debug.Log(roomsCreated.Count, roomsCreated[roomsCreated.Count - 1]);
            }
            else
            {
                Vector3 pos = new Vector3(room.gridPos.x * (roomDimensions.x + gutterSize.x), room.gridPos.y * (roomDimensions.y + gutterSize.y), 0);
                RoomInstance myRoom = Instantiate(RoomObj, pos, Quaternion.identity).GetComponent<RoomInstance>();
                myRoom.Setup(lastSheet, room.gridPos, room.type, room.doorTop, room.doorBot, room.doorLeft, room.doorRight);
                roomsCreated.Add(myRoom.gameObject);
                //Debug.Log(roomsCreated.Count, roomsCreated[roomsCreated.Count - 1]);
            }
        }
    }
}
