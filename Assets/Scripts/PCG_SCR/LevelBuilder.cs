using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public BasicRoomScript startRoomPrefab, endRoomPrefab;
    public List<BasicRoomScript> roomPrefabs = new List<BasicRoomScript>();
    public Vector2 iterationRange = new Vector2(99, 100);

    List<Doorways> availableDoorways = new List<Doorways>();

    StartRoom startRoom;
    EndRoom endRoom;
    List<BasicRoomScript> placedRooms = new List<BasicRoomScript>();

    LayerMask RoomLayerMask;

    void Start()
    {
        RoomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //Place start room
        PlaceStartRoom();
        yield return interval;

        //Random iterations
        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i =0; i < iterations; i++)
        {

            //Place random room from list
            PlaceRoom();
            yield return interval;
        }

        //Place end room
        PlaceEndRoom();
        yield return interval;

        //Level generation finished
        Debug.Log("Level generation finished");
        yield return new WaitForSeconds (3);
        ResetLevelGenerator();
    }

    void PlaceStartRoom()
    {
        //Instatiate room
        startRoom = Instantiate(startRoomPrefab) as StartRoom;
        startRoom.transform.parent = this.transform;

        //Get doorways from current room and add them randomly to list of available doorways
        AddDoorwaysToList(startRoom, ref availableDoorways);

        //Position room
        startRoom.transform.position = Vector3.zero;
		startRoom.transform.rotation = Quaternion.identity;
    }

    void AddDoorwaysToList(BasicRoomScript room, ref List<Doorways> list)
    {
        foreach (Doorways doorway in room.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }

    void PlaceRoom()
    {
        //Instanitate room
        BasicRoomScript currentRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Count)]) as BasicRoomScript;
        currentRoom.transform.parent = this.transform;

        //Create doorway lists to loop over
        List<Doorways> allAvailableDoorways = new List<Doorways>(availableDoorways);
        List<Doorways> currentRoomDoorways = new List<Doorways>();
		AddDoorwaysToList (currentRoom, ref currentRoomDoorways);

		//Get doorways from current room and add them randomly to the list of available doorways
		AddDoorwaysToList(currentRoom, ref availableDoorways);

		bool roomPlaced = false;

		//Try all available doorways
		foreach(Doorways availableDoorway in allAvailableDoorways)
		{
			//Try all available doorways in current room
			foreach(Doorways currentDoorway in currentRoomDoorways)
			{
				//Position room
				PositionRoomAtDoorway(ref currentRoom, currentDoorway, availableDoorway);

				//Check room overlaps
				if(CheckRoomOverlap(currentRoom)) 
				{
					continue;
				}

				roomPlaced = true;

				//Add room to list
				placedRooms.Add(currentRoom);

				//Rmove occupied doorways
				currentDoorway.gameObject.SetActive(false);
				availableDoorways.Remove (currentDoorway);

				availableDoorway.gameObject.SetActive (false);
				availableDoorways.Remove (availableDoorway);

				//Exit loop if room has been placed
				break;
			}

			//Exit loop if room has been placed
			if(roomPlaced)
			{
				break;
			}
		}

		//Room couldn't be placed. Restart generator and try again
		if(!roomPlaced)
		{
			Destroy (currentRoom.gameObject);
			ResetLevelGenerator ();
		}
    }

	void PositionRoomAtDoorway(ref BasicRoomScript room, Doorways roomDoorways, Doorways targetDoorway)
	{
		//Reset room position and rotation
		room.transform.position = Vector3.zero;
		room.transform.rotation = Quaternion.identity;

		//Rotate room to match previous doorway orientation
		Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
		Vector3 roomDoorwaysEuler = roomDoorways.transform.eulerAngles;
		float deltaAngle = Mathf.DeltaAngle (roomDoorwaysEuler.y, targetDoorwayEuler.y);
		Quaternion currentRoomTargetRotation = Quaternion.AngleAxis (deltaAngle, Vector3.up);
		room.transform.rotation = currentRoomTargetRotation * Quaternion.Euler (0, 180f, 0);

		//Position room
		Vector3 roomPositionOffset = roomDoorways.transform.position - room.transform.position;
		room.transform.position = targetDoorway.transform.position - roomPositionOffset;
	}

	bool CheckRoomOverlap(BasicRoomScript room) 
	{
		Bounds bounds = room.RoomBounds;
		bounds.Expand (-0.5f);

		Collider[] colliders = Physics.OverlapBox (bounds.center, bounds.size * 0.5f, room.transform.rotation, RoomLayerMask);
		if (colliders.Length > 0) 
		{
			//Ignore collisions with current room
			foreach(Collider c in colliders)
			{
				if (c.transform.parent.gameObject.Equals (room.gameObject)) {
					continue;
				} else 
				{
					Debug.LogError ("Overlap detected");
					return true;
				}
			}
		}
		return false;
	}

    void PlaceEndRoom()
    {
        //Instanitate room
        endRoom = Instantiate(endRoomPrefab) as EndRoom;
        endRoom.transform.parent = this.transform;

        //Create doorway lists to loop over
        List<Doorways> allAvailableDoorways = new List<Doorways>(availableDoorways);
        Doorways doorway = endRoom.doorways[0];

        bool roomPlaced = false;

        //Try all available doorways
        foreach (Doorways availableDoorway in allAvailableDoorways)
        {
            //Position room
            BasicRoomScript room = (BasicRoomScript)endRoom;
            PositionRoomAtDoorway(ref room, doorway, availableDoorway);

            //Check room overlaps
            if (CheckRoomOverlap(endRoom))
            {
                continue;
            }

            roomPlaced = true;

            //Remove occupied doorways
            doorway.gameObject.SetActive(false);
            availableDoorways.Remove(doorway);

            availableDoorway.gameObject.SetActive(false);
            availableDoorways.Remove(availableDoorway);

            //Exit loop if room has been placed
            break;
        }

        //Room couldn't be placed. Restart generator and try again
        if (!roomPlaced)
        {
            ResetLevelGenerator();
        }
    }

    void ResetLevelGenerator()
    {
        
    }
}
