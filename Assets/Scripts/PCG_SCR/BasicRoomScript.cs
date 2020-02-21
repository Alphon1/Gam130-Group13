using UnityEngine;

public class BasicRoomScript : MonoBehaviour {

    public Doorways[] doorways;
    public BoxCollider boxCollider;

    public Bounds RoomBounds
    {
        get { return boxCollider.bounds; }
    }

}
