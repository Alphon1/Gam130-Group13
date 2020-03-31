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
}
