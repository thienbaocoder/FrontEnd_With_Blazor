public class RoomVM
{
    public static int id { get; set; } = 0;
    public string roomId { get; set; }
    public string name { get; set; }
    public RoomVM()
    {
        roomId = id.ToString();
        name = $"Room {roomId}";
        id++;
    }

}