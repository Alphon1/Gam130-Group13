using UnityEngine;

public class BasicRoomScript : MonoBehaviour {

    public Doorways[] doorways;
    public BoxCollider boxCollider;
    public string[] roomTypes;
    public string roomType;
    public int index;

    public Bounds RoomBounds
    {
        get { return boxCollider.bounds; }
    }

    public void Start()
    {
        roomTypes = new string[3];
        roomTypes[0] = "healthRoom";
        roomTypes[1] = "questRoom";
        roomTypes[2] = "encounterRoom";

        index = Random.Range(0, roomTypes.Length);
        roomType = roomTypes[index];
        print(roomType);
    }
}
