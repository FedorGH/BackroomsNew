using System.Collections.Generic;
using UnityEngine;

public class GenerateManager : MonoBehaviour
{
    public GameObject roomPrefab;
    public int levelSize = 10;
    public int maxConnections = 4;

    private List<Room> rooms = new List<Room>();

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        // Create the initial room
        Room initialRoom = new Room(Vector3.zero);
        rooms.Add(initialRoom);

        // Generate additional rooms
        for (int i = 1; i < levelSize; i++)
        {
            Room newRoom = new Room(Random.insideUnitCircle * 10f); // Random position within a radius of 10 units
            ConnectRooms(newRoom);
            rooms.Add(newRoom);
        }

        // Instantiate room prefabs
        foreach (Room room in rooms)
        {
            Instantiate(roomPrefab, room.position, Quaternion.identity);
        }
    }

    private void ConnectRooms(Room newRoom)
    {
        // Randomly connect the new room to existing rooms
        int connections = Random.Range(1, Mathf.Min(maxConnections + 1, rooms.Count));
        List<Room> availableRooms = new List<Room>(rooms);

        // Shuffle the list of available rooms
        for (int i = 0; i < availableRooms.Count; i++)
        {
            Room temp = availableRooms[i];
            int randomIndex = Random.Range(i, availableRooms.Count);
            availableRooms[i] = availableRooms[randomIndex];
            availableRooms[randomIndex] = temp;
        }

        // Connect the new room to the first N available rooms
        for (int i = 0; i < connections; i++)
        {
            Room existingRoom = availableRooms[i];
            existingRoom.Connect(newRoom);
            newRoom.Connect(existingRoom);
        }
    }
}

public class Room
{
    public Vector3 position;
    public List<Room> connectedRooms = new List<Room>();

    public Room(Vector3 position)
    {
        this.position = position;
    }

    public void Connect(Room room)
    {
        if (!connectedRooms.Contains(room))
        {
            connectedRooms.Add(room);
        }
    }
}
